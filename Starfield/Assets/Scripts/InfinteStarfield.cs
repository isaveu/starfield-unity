//Source: http://www.blenderfreak.com/blog/post/flythrough-camera-smooth-movement-unity3d/
using UnityEngine;

public class InfinteStarfield : MonoBehaviour {

    public ParticleSystem ParticleSystem;
    private Transform tx;
    private ParticleSystem.Particle[] points;

    public int starsMax = 100;
    public float starSize = 1;
    public float starDistance = 10;
    public float starClipDistance = 1;
    private float starDistanceSqr;
    private float starClipDistanceSqr;


    void Start() {
        tx = transform;
        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;
    }

    private void CreateStars() {
        points = new ParticleSystem.Particle[starsMax];

        for (int i = 0; i < starsMax; i++) {
            points[i].position = Random.insideUnitSphere * starDistance + tx.position;
            points[i].color = new Color(1, 1, 1, 1);
            points[i].size = starSize;
        }
    }

    void Update() {
        if (points == null) CreateStars();

        for (int i = 0; i < starsMax; i++) {

            if ((points[i].position - tx.position).sqrMagnitude > starDistanceSqr) {
                points[i].position = Random.insideUnitSphere.normalized * starDistance + tx.position;
            }

            if ((points[i].position - tx.position).sqrMagnitude <= starClipDistanceSqr) {
                float percent = (points[i].position - tx.position).sqrMagnitude / starClipDistanceSqr;
                points[i].color = new Color(1, 1, 1, percent);
                points[i].size = percent * starSize;
            }
        }

        ParticleSystem.SetParticles(points, points.Length);
    }

}