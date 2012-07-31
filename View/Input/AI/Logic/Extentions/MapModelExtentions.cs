using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Input.AI.Logic.MapModel;

namespace View.Input.AI.Logic.Extentions
{
    static class MapModelExtentions
    {
        public static IEnumerable<MapNode> BFS(this MapNode head)
        {
            Queue<MapNode> frontLine = new Queue<MapNode>();
            ISet<MapNode> visited = new HashSet<MapNode>();

            frontLine.Enqueue(head);
            while (frontLine.Count > 0)
            {
                MapNode visiting = frontLine.Dequeue();
                yield return visiting;
                visited.Add(visiting);

                foreach (MapNode neighbor in visiting.GetNeighbors())
                {
                    if (!visited.Contains(neighbor))
                        frontLine.Enqueue(neighbor);
                }
            }
        }
    }
}
