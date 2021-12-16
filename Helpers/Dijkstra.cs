using System;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2021.Helpers
{
    public class Dijkstra
    {
        public static (Dictionary<TState, int>, TState) RunDijkstra<TState>(
		TState start,
		Func<TState, IEnumerable<(TState state, int cost)>> getNextStates,
		Func<Dictionary<TState, int>, TState, bool> endingCondition) where TState : DijkstraNode, IComparable<TState>
	    {
            var totalCost = new Dictionary<TState, int>();
            var pq = new PriorityQueue<TState>();
            pq.Enqueue(start);

            TState p = default;
            while (pq.HasValue)
            {
                p = pq.Dequeue();
                var cost = p.Cost;

                while (pq.Count != 0 && totalCost.ContainsKey(p)){
                    p = pq.Dequeue();
                    cost = p.Cost;
                }

                totalCost[p] = cost;
                if (endingCondition(totalCost, p))
                    break;

                var states = getNextStates(p)
                    .Select(s => (s.state, cost + s.cost));

                foreach (var st in states)
                {
                    pq.Enqueue(st.state);
                }
            }

		    return (totalCost, p);
	    }
    }

    public class DijkstraNode{
        public int Cost { get; set; }
    }
}