/*
===========================================================================
Copyright (C) 2015-2019 Project Meteor Dev Team

This file is part of Project Meteor Server.

Project Meteor Server is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Project Meteor Server is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Project Meteor Server. If not, see <https:www.gnu.org/licenses/>.
===========================================================================
*/

using System;
using System.Collections.Generic;
using Meteor.Common;

namespace Meteor.Map.actors.chara.ai
{
    // Decides when the Mover sends a 0x00CF. The client walks an actor straight toward the last 0x00CF
    // target. Near that target it starts its stop and ignores new targets until it has stopped, so a
    // target only one tick ahead makes it stop and go. Retail sends a target well ahead and the next one
    // before the actor gets near it: one per straight stretch when roaming, about every 2 s when chasing.
    //
    // It also tracks where the client draws the actor, including its speed-up from standstill, so range
    // and aggro checks see what the player sees.
    class MoveCadence
    {
        // Hop length in seconds of travel. With LEAD_SECONDS this gives about retail's 1.8 s between chase hops.
        public const double HOP_SECONDS = 2.4;
        // The next hop goes out while the client still has this long to walk, before it starts
        // stopping and would ignore it.
        public const double LEAD_SECONDS = 0.6;
        // The client warps an actor to a 0x00CF more than 30 yalms away.
        public const float MAX_HOP = 20.0f;
        public const float MIN_HOP = 2.0f;
        // A sharper turn ends the hop, since the client walks straight and cutting a corner goes through walls.
        public const float CORNER_DEGREES = 12.0f;
        // The route changed under the hop when the server is this far from where the client draws
        // the actor: send again.
        public const float DRIFT = 2.0f;
        // Send again when the target is this far off the current route.
        public const float RETARGET = 1.0f;
        // Retail sends the stop a moment after the actor arrives.
        public const double STOP_DELAY = 0.5;
        // How long the client takes to reach full speed from standstill.
        public const double ACCEL_SECONDS = 1.0;

        public bool Active { get; private set; }
        private double startWait = double.NaN;
        private double crowdWait = double.NaN;
        public Vector3 From { get; private set; }
        public Vector3 Target { get; private set; }
        public double SentAt { get; private set; }
        public double WalkStart { get; private set; }
        public float Speed { get; private set; }
        public ushort State { get; private set; }
        public bool Final { get; private set; }
        public bool Corner { get; private set; }
        private Vector3 ClientNow(double now) => ClientPos(now);

        public void Sent(Vector3 from, Vector3 target, float speed, ushort state, double now, bool final, bool corner = false)
        {
            startWait = double.NaN;
            crowdWait = double.NaN;
            if (!Active)
                WalkStart = now;
            Active = true;
            From = new Vector3(from.X, from.Y, from.Z);
            Target = new Vector3(target.X, target.Y, target.Z);
            Speed = speed;
            State = state;
            SentAt = now;
            Final = final;
            Corner = corner;
        }

        public void Reset()
        {
            startWait = double.NaN;
            crowdWait = double.NaN;
            Active = false;
            From = null;
            Target = null;
            Final = false;
            Corner = false;
        }

        // Distance walked t seconds after starting from standstill, on the client's speed curve.
        public static float Ramp(float speed, double t)
        {
            if (speed <= 0.0f || t <= 0.0)
                return 0.0f;
            if (t >= ACCEL_SECONDS)
                return (float)(speed * (t - ACCEL_SECONDS / 3.0));
            return (float)(speed * (2.0 / 3.0) * t * Math.Sqrt(t / ACCEL_SECONDS));
        }

        public static float Step(float speed, double t0, double t1)
        {
            return Math.Max(0.0f, Ramp(speed, t1) - Ramp(speed, Math.Max(0.0, t0)));
        }

        public Vector3 ClientPos(double now)
        {
            if (!Active)
                return null;
            float length = MoverMath.XZDistance(From, Target);
            float walked = Step(Speed, SentAt - WalkStart, now - WalkStart);
            if (length < 0.0001f || walked >= length)
                return new Vector3(Target.X, Target.Y, Target.Z);
            float k = walked / length;
            return new Vector3(From.X + (Target.X - From.X) * k, From.Y + (Target.Y - From.Y) * k, From.Z + (Target.Z - From.Z) * k);
        }

        public double ArrivesAt()
        {
            if (!Active)
                return double.NegativeInfinity;
            float length = MoverMath.XZDistance(From, Target);
            if (Speed <= 0.0f || length < 0.0001f)
                return SentAt;
            double lo = SentAt, hi = SentAt + length / Speed + ACCEL_SECONDS;
            for (int i = 0; i < 40; i++)
            {
                double mid = (lo + hi) * 0.5;
                if (Step(Speed, SentAt - WalkStart, mid - WalkStart) >= length)
                    hi = mid;
                else
                    lo = mid;
            }
            return hi;
        }

        public static float LeadDistance(float speed, double tickDt)
        {
            return Math.Max(0.05f, MoverMath.Budget(speed, LEAD_SECONDS + tickDt));
        }

        public enum Reason { None, Start, State, Speed, Reach, Drift, Retarget }

        public Reason Check(double now, Vector3 serverPos, float speed, ushort state, double tickDt, bool routeGoesOn, float targetOffRoute)
        {
            if (!Active)
                return Reason.Start;
            if (state != State)
                return Reason.State;
            if (Math.Abs(speed - Speed) > 0.01f)
                return Reason.Speed;
            Vector3 client = ClientNow(now);
            if (MoverMath.XZDistance(client, serverPos) > DRIFT)
                return Reason.Drift;
            if (targetOffRoute > RETARGET)
                return Reason.Retarget;
            if (routeGoesOn && MoverMath.XZDistance(client, Target) <= LeadDistance(speed, tickDt))
                return Reason.Reach;
            return Reason.None;
        }

        public struct Hop
        {
            public bool Send;
            public Reason Why;
            public Vector3 Target;
            public bool Final;
            // A hop was due but every line failed ChordValidator, so nothing was sent.
            public bool Blocked;
            public ChordResult Check;
            public bool Checked;
            public int Shortened;
            public ChordResult FirstFail;
        }

        public const float SHORTEN_BACK = 0.5f;

        // Pulls the hop's end back to just before each failure until the line passes. Blocked when
        // nothing is left of it.
        public static bool Shorten(ref Hop hop, Vector3 c, float length, Func<float, Vector3> pointAt, Func<Vector3, Vector3, ChordResult> chord)
        {
            hop.Checked = true;
            Vector3 t = hop.Target;
            float at = length;
            for (int guard = 0; guard < 64; guard++)
            {
                var r = chord(c, t);
                hop.Check = r;
                if (r.Ok)
                {
                    hop.Target = t;
                    return true;
                }
                if (hop.FirstFail.Reason == ChordResult.Why.Ok)
                    hop.FirstFail = r;
                if (r.Reason == ChordResult.Why.TooShort)
                {
                    // The client is already heading there.
                    hop.Send = false;
                    return false;
                }
                float next = r.Length > 1e-4f ? Math.Min(at - SHORTEN_BACK, at * (r.S / r.Length) - SHORTEN_BACK) : at - SHORTEN_BACK;
                if (next <= 0.0f)
                    break;
                at = next;
                t = pointAt(at);
                hop.Shortened++;
                if (MoverMath.XZDistance(t, c) < ChordValidator.MIN_MOVE)
                    break;
            }
            hop.Send = false;
            hop.Blocked = true;
            return false;
        }

        public static Vector3 Along(Vector3 a, Vector3 b, float dist)
        {
            float len = MoverMath.XZDistance(a, b);
            float k = len < 1e-4f ? 0.0f : Math.Min(1.0f, dist / len);
            return new Vector3(a.X + (b.X - a.X) * k, a.Y + (b.Y - a.Y) * k, a.Z + (b.Z - a.Z) * k);
        }

        // A walk sends one hop per straight stretch; a run goes about HOP_SECONDS ahead.
        public static float MaxHop(float speed, bool walk)
        {
            if (walk)
                return MAX_HOP;
            return Math.Min(MAX_HOP, Math.Max(MIN_HOP, MoverMath.Budget(speed, HOP_SECONDS)));
        }

        // This tick's hop decision. done: the walk ends at pos. truncated: the route is one part of a longer
        // trip. maxDy: how much the ground may rise or fall along a hop (NaN: any). A hop past a corner goes
        // out early only when lineClear says the client can cut it. A hop that fails chord is pulled back,
        // and one that can't pass comes back Blocked.
        public Hop Plan(double now, Vector3 pos, IList<Vector3> points, int index, bool done, bool truncated,
            float speed, ushort state, bool walk, double tickDt, float maxDy = float.NaN, Func<Vector3, Vector3, bool> lineClear = null,
            double minResend = 0.0, Func<Vector3, Vector3, ChordResult> chord = null, Vector3 clientAt = null)
        {
            var hop = new Hop();
            bool corner = false;
            float off = 0.0f;
            if (done)
            {
                if (Active && State == state && MoverMath.XZDistance(Target, pos) <= 0.05f)
                    return hop;
                if (Active && State == state && now - SentAt < minResend && MoverMath.XZDistance(Target, pos) <= DRIFT)
                    return hop;     // the stop goes out with the real position
                hop.Send = true;
                hop.Why = Active ? Reason.Retarget : Reason.Start;
                hop.Target = new Vector3(pos.X, pos.Y, pos.Z);
                hop.Final = true;
                if (chord != null)
                {
                    Vector3 c = clientAt ?? (Active ? ClientNow(now) : pos);
                    Vector3 end = hop.Target;
                    if (!Shorten(ref hop, c, MoverMath.XZDistance(c, end), d => Along(c, end, d), chord))
                        return hop;
                    hop.Final = MoverMath.XZDistance(hop.Target, end) < 0.01f;
                }
            }
            else
            {
                off = Active ? DistanceToRoute(Target, pos, points, index) : 0.0f;
                bool goesOn = !Final || (points.Count > 0 && MoverMath.XZDistance(points[points.Count - 1], Target) > 0.05f);
                hop.Why = Check(now, pos, speed, state, tickDt, goesOn, off);
                if (hop.Why == Reason.None)
                    return hop;
                // A chaser right behind another waits a tick, so it falls back without stopping.
                if (hop.Why == Reason.Reach && HoldHop != null
                    && MoverMath.XZDistance(ClientNow(now), Target) > MoverMath.Budget(speed, tickDt * HOLD_TICKS) && HoldHop())
                    return new Hop();
                // Chasers starting from one spot would run on top of each other; the crowded one waits.
                if (hop.Why == Reason.Start && HoldHop != null && HoldHop())
                {
                    if (double.IsNaN(crowdWait))
                        crowdWait = now;
                    if (now - crowdWait < HOLD_START)
                        return new Hop();
                }
                // A chase re-plans every tick, so limit re-sends. Not for Reach: the client would stop and start again.
                if ((hop.Why == Reason.Retarget || hop.Why == Reason.Drift) && now - SentAt < minResend)
                    return new Hop();
                // The target just started moving: wait for its speed to be known rather than send a tiny hop.
                if (hop.Why == Reason.Start && minResend > 0.0
                    && MoverMath.XZDistance(pos, points[points.Count - 1]) < LeadDistance(speed, tickDt) + MoverMath.Budget(speed, minResend))
                {
                    if (double.IsNaN(startWait))
                        startWait = now;
                    if (now - startWait < FOLLOW_SPAN)
                        return new Hop();
                }
                HopEnd end;
                int after = Corner && hop.Why == Reason.Reach ? PointAfter(points, index, Target) : -1;
                Vector3 origin = pos;
                int originIndex = index;
                if (after >= 0)
                {
                    // Send the hop past the corner early only if cutting the corner doesn't go through a wall.
                    origin = Target;
                    originIndex = after;
                    hop.Target = NextHopOrEnd(Target, points, after, MaxHop(speed, walk), maxDy, out end);
                    Vector3 client = ClientNow(now);
                    if (lineClear != null && MoverMath.XZDistance(client, Target) > 0.05f && !lineClear(client, hop.Target))
                        return new Hop();
                }
                else
                {
                    hop.Target = NextHopOrEnd(pos, points, index, MaxHop(speed, walk), maxDy, out end);
                    // The new route's next corner can be short of where the client is already going.
                    // Carry on from that target instead, or the client turns back.
                    int on = hop.Why == Reason.Reach && end == HopEnd.Corner ? SegmentAfter(points, index, pos, Target) : -1;
                    if (on >= 0)
                    {
                        Vector3 client = ClientNow(now);
                        if (MoverMath.XZDistance(client, hop.Target) < MoverMath.XZDistance(client, Target) + MIN_HOP)
                        {
                            origin = Target;
                            originIndex = on;
                            hop.Target = NextHopOrEnd(Target, points, on, MaxHop(speed, walk), maxDy, out end);
                        }
                    }
                }
                hop.Send = true;
                hop.Final = end == HopEnd.RouteEnd && !truncated;
                corner = end == HopEnd.Corner;
                if (chord != null)
                {
                    Vector3 c = clientAt ?? (Active ? ClientNow(now) : pos);
                    Vector3 o = origin;
                    int oi = originIndex;
                    Vector3 planned = hop.Target;
                    if (corner && ThroughCorners(ref hop, c, o, points, oi, MaxHop(speed, walk), maxDy, chord, out bool routeEnd))
                    {
                        corner = false;
                        hop.Final = routeEnd && !truncated;
                    }
                    else
                    {
                        if (!Shorten(ref hop, c, MoverMath.XZDistance(o, planned), d => NextHop(o, points, oi, d, maxDy, out _), chord))
                            return hop;
                        if (hop.Shortened > 0)
                        {
                            hop.Final = false;
                            corner = false;
                        }
                    }
                }
            }
            // The client ignores a target this close; re-sending it only turns the actor in place.
            if (Active && State == state && MoverMath.XZDistance(hop.Target, Target) < ChordValidator.MIN_MOVE)
                return new Hop();
            // Inside minResend, only send a hop that really leads on; small wiggles turn the actor in place.
            if (Active && hop.Why == Reason.Reach && now - SentAt < minResend
                && (MoverMath.XZDistance(hop.Target, Target) < MIN_HOP || Behind(hop.Target, ClientNow(now), From, Target) > BEHIND_SLACK))
                return new Hop();
            // Skip a re-route that only cuts the client's walk short: it would stop and turn for nothing.
            if (!done && Active && hop.Why == Reason.Retarget && minResend > 0.0 && off <= CHASE_RETARGET)
            {
                Vector3 client = ClientNow(now);
                float left = MoverMath.XZDistance(client, Target);
                float reach = MoverMath.XZDistance(client, hop.Target);
                if (reach < left || (left > LeadDistance(speed, tickDt) && reach < left + MIN_HOP))
                    return new Hop();
            }
            Vector3 from = !Active ? pos : ClientNow(now);
            Sent(from, hop.Target, speed, state, now, hop.Final, corner);
            return hop;
        }

        public const double HOLD_START = 0.7;

        // True holds back the next hop (a chaser crowding the one ahead).
        public Func<bool> HoldHop;

        public const double HOLD_TICKS = 1.5;

        public const float CHASE_RETARGET = 3.0f;

        public const float BEHIND_SLACK = 0.5f;

        public const float THROUGH_GAIN = 0.5f;
        // The route keeps large bodies off walls at bends, so a hop through them stays this close to it.
        public const float THROUGH_LANE = 0.5f;

        private static bool InLane(Vector3 c, Vector3 origin, Vector3 t, IList<Vector3> points, int index, float routeLength)
        {
            if (DistanceToSegment(origin, c, t) > THROUGH_LANE)
                return false;
            float walked = 0.0f;
            Vector3 prev = origin;
            for (int i = index; i < points.Count; i++)
            {
                walked += MoverMath.XZDistance(prev, points[i]);
                prev = points[i];
                if (walked >= routeLength - 0.01f)
                    break;
                if (DistanceToSegment(points[i], c, t) > THROUGH_LANE)
                    return false;
            }
            return true;
        }

        // Extends a hop that would stop at a corner on past it while the straight line stays safe.
        // True when that gains at least THROUGH_GAIN.
        public static bool ThroughCorners(ref Hop hop, Vector3 c, Vector3 origin, IList<Vector3> points, int index, float maxLength,
            float maxDy, Func<Vector3, Vector3, ChordResult> chord, out bool routeEnd)
        {
            routeEnd = false;
            float d = maxLength;
            Vector3 t = NextHop(origin, points, index, d, maxDy, out HopEnd end, corners: false);
            while (d > MIN_HOP && !InLane(c, origin, t, points, index, d))
            {
                d -= SHORTEN_BACK;
                t = NextHop(origin, points, index, d, maxDy, out end, corners: false);
            }
            if (!InLane(c, origin, t, points, index, d))
                return false;
            var trial = new Hop { Send = true, Target = t };
            if (!Shorten(ref trial, c, d, x => NextHop(origin, points, index, x, maxDy, out _, corners: false), chord))
                return false;
            if (MoverMath.XZDistance(c, trial.Target) < MoverMath.XZDistance(c, hop.Target) + THROUGH_GAIN)
                return false;
            hop.Target = trial.Target;
            hop.Check = trial.Check;
            hop.Checked = true;
            hop.Shortened = trial.Shortened;
            routeEnd = end == HopEnd.RouteEnd && trial.Shortened == 0;
            return true;
        }

        private static int PointAfter(IList<Vector3> points, int index, Vector3 p)
        {
            for (int i = Math.Max(0, index - 1); i < points.Count - 1; i++)
                if (MoverMath.XZDistance(points[i], p) < 0.01f)
                    return i + 1;
            return -1;
        }

        // Avoids leaving a tiny hop after this one.
        public static Vector3 NextHopOrEnd(Vector3 pos, IList<Vector3> points, int index, float maxLength, float maxDy, out HopEnd end)
        {
            Vector3 longer = NextHop(pos, points, index, maxLength + MIN_HOP, maxDy, out end);
            if (end == HopEnd.RouteEnd || end == HopEnd.Corner)
                return longer;
            return NextHop(pos, points, index, maxLength, maxDy, out end);
        }

        public const float CORNER_SKIP = 1.5f;

        public enum HopEnd { Length, RouteEnd, Corner, Height }

        // Ends at maxLength, a corner, or where the ground rises or falls more than maxDy.
        public static Vector3 NextHop(Vector3 pos, IList<Vector3> points, int index, float maxLength, float maxDy, out HopEnd end, bool corners = true)
        {
            end = HopEnd.Length;
            double cosLimit = Math.Cos(CORNER_DEGREES * Math.PI / 180.0);
            Vector3 cur = new Vector3(pos.X, pos.Y, pos.Z);
            float left = maxLength;
            float hx = 0.0f, hz = 0.0f;
            bool heading = false;
            float yMin = pos.Y, yMax = pos.Y;
            float walked = 0.0f;
            for (int i = index; i < points.Count; i++)
            {
                Vector3 p = points[i];
                float dx = p.X - cur.X, dz = p.Z - cur.Z;
                float seg = (float)Math.Sqrt(dx * dx + dz * dz);
                if (seg < 0.0001f)
                    continue;
                if (heading)
                {
                    if (corners && (dx * hx + dz * hz) / seg < cosLimit)
                    {
                        if (walked >= CORNER_SKIP)
                        {
                            end = HopEnd.Corner;
                            return cur;
                        }
                        // A bend right at the start is just the step back onto the mesh.
                        hx = dx / seg;
                        hz = dz / seg;
                    }
                }
                else if (seg > 0.3f || i == points.Count - 1)
                {
                    hx = dx / seg;
                    hz = dz / seg;
                    heading = true;
                }

                float take = Math.Min(seg, left);
                float t = take / seg;
                if (!float.IsNaN(maxDy))
                {
                    float yAt = cur.Y + (p.Y - cur.Y) * t;
                    float tOut = BandExit(cur.Y, yAt, yMax - maxDy, yMin + maxDy);
                    if (tOut < 1.0f)
                    {
                        float tc = t * tOut;
                        end = HopEnd.Height;
                        return new Vector3(cur.X + dx * tc, cur.Y + (p.Y - cur.Y) * tc, cur.Z + dz * tc);
                    }
                    yMin = Math.Min(yMin, yAt);
                    yMax = Math.Max(yMax, yAt);
                }
                if (take >= seg)
                {
                    cur = new Vector3(p.X, p.Y, p.Z);
                    left -= seg;
                    walked += seg;
                    if (left <= 0.0001f)
                    {
                        end = i == points.Count - 1 ? HopEnd.RouteEnd : HopEnd.Length;
                        return cur;
                    }
                    continue;
                }
                return new Vector3(cur.X + dx * t, cur.Y + (p.Y - cur.Y) * t, cur.Z + dz * t);
            }
            end = HopEnd.RouteEnd;
            return cur;
        }

        public static float BandExit(float y0, float y1, float lo, float hi)
        {
            if (y1 <= hi && y1 >= lo)
                return 1.0f;
            if (y0 > hi || y0 < lo)
                return 0.0f;
            float dy = y1 - y0;
            if (Math.Abs(dy) < 0.0001f)
                return 1.0f;
            float bound = y1 > hi ? hi : lo;
            return Math.Max(0.0f, Math.Min(1.0f, (bound - y0) / dy));
        }

        public static float DistanceToRoute(Vector3 point, Vector3 pos, IList<Vector3> points, int index)
        {
            float best = MoverMath.XZDistance(point, pos);
            Vector3 a = pos;
            for (int i = index; i < points.Count; i++)
            {
                Vector3 b = points[i];
                best = Math.Min(best, DistanceToSegment(point, a, b));
                a = b;
            }
            return best;
        }

        public static int SegmentAfter(IList<Vector3> points, int index, Vector3 pos, Vector3 point)
        {
            int best = -1;
            float bestD = RETARGET;
            Vector3 a = pos;
            for (int i = index; i < points.Count; i++)
            {
                float d = DistanceToSegment(point, a, points[i]);
                if (d <= bestD)
                {
                    bestD = d;
                    best = i;
                }
                a = points[i];
            }
            return best;
        }

        public static float DistanceToSegment(Vector3 p, Vector3 a, Vector3 b)
        {
            float abx = b.X - a.X, abz = b.Z - a.Z;
            float len2 = abx * abx + abz * abz;
            float t = len2 < 0.000001f ? 0.0f : ((p.X - a.X) * abx + (p.Z - a.Z) * abz) / len2;
            t = Math.Max(0.0f, Math.Min(1.0f, t));
            float x = a.X + abx * t - p.X, z = a.Z + abz * t - p.Z;
            return (float)Math.Sqrt(x * x + z * z);
        }

        // A chased target's velocity. Routing to where the target is now gives tiny hops when it runs as
        // fast as the mob; routing to where it will be in HOP_SECONDS gives retail's long hops.
        public class GoalTracker
        {
            private readonly double span;
            private Vector3 prev;
            private double prevAt;

            // A player's position updates come in bursts, so one tick can see two or none.
            public GoalTracker(double span = 0.25)
            {
                this.span = span;
            }

            public float VelX { get; private set; }
            public float VelZ { get; private set; }
            public bool Moving => VelX * VelX + VelZ * VelZ > MOVING_SPEED * MOVING_SPEED;
            public const float MOVING_SPEED = 0.5f;

            // Faster than maxSpeed is a warp, not a velocity.
            public void Track(Vector3 p, double now, float maxSpeed)
            {
                if (prev == null || now - prevAt > 1.5 || now < prevAt)
                {
                    prev = new Vector3(p.X, p.Y, p.Z);
                    prevAt = now;
                    VelX = VelZ = 0.0f;
                    return;
                }
                double dt = now - prevAt;
                if (dt < span)
                    return;
                float vx = (float)((p.X - prev.X) / dt), vz = (float)((p.Z - prev.Z) / dt);
                if ((float)Math.Sqrt(vx * vx + vz * vz) > Math.Max(1.0f, maxSpeed))
                    vx = vz = 0.0f;
                VelX = (VelX + vx) * 0.5f;
                VelZ = (VelZ + vz) * 0.5f;
                prev = new Vector3(p.X, p.Y, p.Z);
                prevAt = now;
            }

            public void Reset()
            {
                prev = null;
                VelX = VelZ = 0.0f;
            }

            public Vector3 Lead(Vector3 p, double seconds)
            {
                return new Vector3(p.X + VelX * (float)seconds, p.Y, p.Z + VelZ * (float)seconds);
            }
        }

        // Where a chased target will be in HOP_SECONDS. Never steps back while the target keeps its heading:
        // measured from single ticks, the goal jumped back and forth and every tick re-sent.
        public class ChaseLead
        {
            private readonly GoalTracker track = new GoalTracker(FOLLOW_SPAN);
            private Vector3 last;
            private float lastUx, lastUz;

            public bool Moving => track.Moving;

            public Vector3 Lead(Vector3 target, double now, float maxSpeed)
            {
                track.Track(target, now, maxSpeed);
                if (!track.Moving)
                {
                    last = null;
                    return target;
                }
                float v = (float)Math.Sqrt(track.VelX * track.VelX + track.VelZ * track.VelZ);
                float ux = track.VelX / v, uz = track.VelZ / v;
                Vector3 led = track.Lead(target, HOP_SECONDS);
                if (last != null && ux * lastUx + uz * lastUz > TURN_COS)
                {
                    float back = (last.X - led.X) * ux + (last.Z - led.Z) * uz;
                    if (back > 0.0f)
                        led = new Vector3(led.X + ux * back, led.Y, led.Z + uz * back);
                }
                last = led;
                lastUx = ux;
                lastUz = uz;
                return led;
            }

            public void Reset()
            {
                track.Reset();
                last = null;
            }

            public void Rebase()
            {
                last = null;
            }

            // cos 45°
            private const float TURN_COS = 0.7071f;
        }

        public const double FOLLOW_SPAN = 0.6;

        // Retail re-sends at turns 0.4-1 s apart.
        public const double FOLLOW_RESEND = 0.9;

        public static float Behind(Vector3 p, Vector3 c, Vector3 a, Vector3 b)
        {
            float abx = b.X - a.X, abz = b.Z - a.Z;
            float len = (float)Math.Sqrt(abx * abx + abz * abz);
            if (len < 0.001f)
                return 0.0f;
            float along = ((p.X - c.X) * abx + (p.Z - c.Z) * abz) / len;
            return Math.Max(0.0f, -along);
        }

        public static float Yaw(Vector3 from, Vector3 to, float fallback)
        {
            if (from.X == to.X && from.Z == to.Z)
                return fallback;
            double rot2 = Math.Atan2(from.Z - to.Z, from.X - to.X);
            return (float)Math.IEEERemainder(Math.PI - rot2 + Math.PI / 2, 2 * Math.PI);
        }
    }
}
