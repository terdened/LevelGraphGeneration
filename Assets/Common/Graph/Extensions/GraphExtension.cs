using System.Linq;
using UnityEngine;

public static class GraphExtension
{
    public static Graph<T> ToFullGraph<T>(this Graph<T> graph)
    {
        var result = new Graph<T>();

        var vertexes = graph.Vertexes.ToList().Select(_ =>
        {
            return new Vertex<T>(_.Data);
        });

        result.AddVertexes(vertexes);

        foreach(var vertex in vertexes)
        {
            var vertexesForEdges = vertexes.Where(_ => _.Id != vertex.Id);
            /*{
                return ;
                && !graph.Edges.Any(e =>
                    (e.Vertexes.ToList()[0].Id == vertex.Id
                    && e.Vertexes.ToList()[1].Id == _.Id)
                    || (e.Vertexes.ToList()[1].Id == vertex.Id
                    && e.Vertexes.ToList()[0].Id == _.Id));
            });*/
            Debug.Log(vertexesForEdges.Count());

            var newEdges = vertexesForEdges.Select(_ => new Edge<T>(vertex, _, 1));
            graph.AddEdges(newEdges);
        }

        return graph;
    }
}
