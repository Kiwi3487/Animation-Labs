using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

namespace Unity.Splines.Examples
{
    public class HighwayExample : MonoBehaviour
    {
        public SplineContainer container;

        [SerializeField]
        float speed = 0.5f;

        SplinePath[] paths = new SplinePath[2]; // Define two paths
        float t = 0f; // Initialize t

        IEnumerator CarPathCoroutine()
        {
            while (true)
            {
 
                t = 0f;
                var path = paths[Random.Range(0, paths.Length)];
                while (t <= 1f)
                {
                    var pos = path.EvaluatePosition(t);
                    var direction = path.EvaluateTangent(t);
                    transform.position = pos;
                    transform.LookAt(pos + direction);
                    t += speed * Time.deltaTime;

                    yield return null;
                }

            }
        }

        void Start()
        {
            var localToWorldMatrix = container.transform.localToWorldMatrix;

            // Define paths
            paths[0] = new SplinePath(new[] {
                new SplineSlice<Spline>(container.Splines[0], new SplineRange(0, 25), localToWorldMatrix)
            });

            paths[1] = new SplinePath(new[] {
                new SplineSlice<Spline>(container.Splines[1], new SplineRange(0, 4), localToWorldMatrix),
                new SplineSlice<Spline>(container.Splines[0], new SplineRange(2, 23), localToWorldMatrix)
            });
            StartCoroutine(CarPathCoroutine()); 
        }
    }
}