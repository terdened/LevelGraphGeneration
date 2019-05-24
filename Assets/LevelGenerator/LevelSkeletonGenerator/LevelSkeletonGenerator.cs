using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSkeletonGenerator : BaseGenerator<LevelSkeleton, LevelSkeletonGeneratorParams>
{
    private readonly SkeletonPointGenerator _skeletonPointGenerator;

    public LevelSkeletonGenerator(LevelSkeletonGeneratorParams generatorParams, IEnumerable<IGeneratorCriteria<LevelSkeleton>> generatorCriteria = null) : base(generatorParams, generatorCriteria)
    {
        var pointGeneratorParams = new SkeletonPointGeneratorParams
        {
            RandomPointsCount = 10,
            MinimalX = -10,
            MaximalX = 10,
            MinimalY = -5,
            MaximalY = 5,
            InitialPoints = new List<Vector2> {
                new Vector2(11, 2),
                new Vector2(-11, -3)
            }
        };
        _skeletonPointGenerator = new SkeletonPointGenerator(pointGeneratorParams);
    }

    protected override LevelSkeleton Generate()
    {
        var result = new LevelSkeleton();

        var points = _skeletonPointGenerator.Execute();

        result.AddPoints(points);

        var newLines = result.ToGraph().ToFullGraph().Edges.ToList().Select(_ =>
        {
            return new SkeletonLine(_.Vertexes.ToList()[0].Data, _.Vertexes.ToList()[1].Data);
        });

        result.AddLines(newLines);

        return result;
    }
}
