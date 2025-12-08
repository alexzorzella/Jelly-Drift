using UnityEngine;

public class CarAI : MonoBehaviour {
    public Transform path;

    public Transform[] nodes;
    public int respawnHeight;
    public int[] difficultyConfig;
    public float xOffset;
    public float speedSteerMultiplier = 1f;
    public float steerMultiplier = 1f;
    public int maxTurnSpeed = 50;
    readonly int maxLookAhead = 6;
    readonly float recoverTime = 1.5f;
    readonly float slowdownM = 5f;
    readonly float speedAdjustMultiplier = 5f;
    readonly float speedupM = 15f;
    readonly int turnLookAhead = 6;
    Car car;
    int currentDriftNode;
    int currentNode;
    int difficulty;
    LineRenderer line;
    int lookAhead = 4;
    float maxOffset = 0.36f;
    int nextTurnLength;
    int nextTurnStart;
    float roadWidth = 0.4f;
    int turnDir;

    void Start() {
        difficulty = (int)GameState.Instance.difficulty;
        print(string.Concat("d: ", GameState.Instance.difficulty, ", a: ", difficulty));
        // car.GetCarData().GetEngineForce() = difficultyConfig[difficulty]; // TODO: Fix
        InvokeRepeating("AdjustSpeed", 0.5f, 0.5f);
        if (GameController.Instance.finalCheckpoint != 0) {
            GetComponent<CheckpointUser>().ForceCheckpoint(0);
        }
    }

    void Update() {
        if (!GameController.Instance.playing || !path) {
            return;
        }

        NewAI();
        CheckRecover();
    }

    public void Recover() {
        car.rb.linearVelocity = Vector3.zero;
        transform.position = nodes[FindClosestNode(3, transform)].position;
        var num = currentNode % nodes.Length;
        var num2 = (num + 1) % nodes.Length;
        var normalized = (nodes[num2].position - nodes[num].position).normalized;
        transform.rotation = Quaternion.LookRotation(normalized);
    }

    void CheckRecover() {
        if (!GameController.Instance.playing) {
            return;
        }

        if (transform.position.y < respawnHeight) {
            Recover();
        }

        if (IsInvoking("Recover")) {
            if (car.speed > 3f) {
                CancelInvoke("Recover");
            }

            return;
        }

        if (car.speed < 3f) {
            Invoke("Recover", recoverTime);
            return;
        }

        CancelInvoke("Recover");
    }

    public void SetPath(Transform p) {
        path = p;
        nodes = path.GetComponentsInChildren<Transform>();
        car = GetComponent<Car>();
        currentNode = FindClosestNode(nodes.Length, transform);
    }

    int FindNextTurn() {
        for (var i = currentNode; i < currentNode + turnLookAhead; i++) {
            var num = i % nodes.Length;
            var num2 = (num + 1) % nodes.Length;
            var num3 = (num2 + 1) % nodes.Length;
            var vector = nodes[num2].position - nodes[num].position;
            var vector2 = nodes[num3].position - nodes[num2].position;
            var f = Vector3.SignedAngle(vector.normalized, vector2.normalized, Vector3.up);
            if (Mathf.Abs(f) > 20f) {
                turnDir = (int)Mathf.Sign(f);
                nextTurnLength = FindNextStraight(num2);
                return num2;
            }
        }

        return -1;
    }

    int FindNextStraight(int startNode) {
        for (var i = startNode; i < startNode + turnLookAhead; i++) {
            var num = i % nodes.Length;
            var num2 = (num + 1) % nodes.Length;
            var num3 = (num2 + 1) % nodes.Length;
            var from = nodes[num2].position - nodes[num].position;
            var to = nodes[num3].position - nodes[num2].position;
            if (Mathf.Abs(Vector3.SignedAngle(from, to, Vector3.up)) < 15f) {
                return num2 - startNode;
            }
        }

        return 3;
    }

    void NewAI() {
        var num = FindClosestNode(maxLookAhead, transform);
        currentNode = num;
        var num2 = (num + 1) % nodes.Length;
        if (currentNode > nextTurnStart + nextTurnLength) {
            nextTurnStart = FindNextTurn();
        }

        if (num2 < nextTurnStart) {
            xOffset = 0.13f * turnDir;
        }
        else if (num2 >= nextTurnStart && num2 < nextTurnStart + nextTurnLength) {
            xOffset = -0.13f * turnDir;
        }
        else {
            xOffset = 0f;
        }

        var b = Vector3.Cross(nodes[num2].position - nodes[num].position, Vector3.up) * xOffset;
        var vector = nodes[num2].position + b - transform.position;
        vector = transform.InverseTransformDirection(vector);
        var num3 = 1f + Mathf.Clamp(car.speed * 0.01f * speedSteerMultiplier, 0f, 1f);
        car.steering = Mathf.Clamp(vector.x * 0.05f * num3, -1f, 1f) * num3;
        car.throttle = 1f;
        car.throttle = 1f - Mathf.Abs(car.steering * Mathf.Clamp(car.speed - maxTurnSpeed, 0f, 100f) * 0.06f);
    }

    void AdjustSpeed() {
        var num = FindClosestNode(nodes.Length, transform) / (float)nodes.Length;
        var num2 = FindClosestNode(nodes.Length, GameController.Instance.currentCar.transform) / (float)nodes.Length;
        var num3 = num - num2;
        if (num3 < 0f) {
            num3 *= speedupM;
        }

        if (num3 > 0f) {
            num3 *= slowdownM;
        }

        var num4 = difficultyConfig[difficulty] - Mathf.Clamp(num3 * 1000f * speedAdjustMultiplier, -8000f, 4000f);
        num4 = Mathf.Clamp(num4, 1000f, 8000f);
        // car.GetCarData().GetEngineForce() = num4; TODO: Fix
    }

    int FindClosestNode(int maxLook, Transform target) {
        var num = float.PositiveInfinity;
        var result = 0;
        for (var i = 0; i < maxLook; i++) {
            var num2 = (currentNode + i) % nodes.Length;
            var num3 = Vector3.Distance(target.position, nodes[num2].position);
            if (num3 < num) {
                num = num3;
                result = num2;
            }
        }

        return result;
    }
}