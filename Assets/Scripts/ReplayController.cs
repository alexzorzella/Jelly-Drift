using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReplayController : MonoBehaviour {
    public static ReplayController Instance;
    float endTime;
    string filePath;

    List<ReplayFrame> replay;
    float startTime;
    public int hz { get; set; } = 30;
    public Car car { get; set; }

    void Awake() {
        Instance = this;
    }

    void Start() {
        var path = Application.persistentDataPath + "/replays";
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        filePath = string.Concat(Application.persistentDataPath, "/replays/pb", GameState.Instance.map, ".txt");
        replay = new List<ReplayFrame>();
        startTime = Time.time;
    }

    void Update() {
    }

    void FixedUpdate() {
        if (!GameController.Instance || !car) {
            return;
        }

        replay.Add(new ReplayFrame(car.rb.position, car.rb.rotation.eulerAngles, car.steerAngle, Time.time));
    }

    public void Save() {
        print("saving");
        var streamWriter = StreamWriter.Null;
        try {
            streamWriter = new StreamWriter(filePath, false);
            streamWriter.WriteLine(GameState.Instance.car + ", " + GameState.Instance.skin);
            foreach (var replayFrame in replay) {
                streamWriter.WriteLine(string.Concat(replayFrame.pos, ", ", replayFrame.rot, ", ",
                    replayFrame.steerAngle, ",", replayFrame.time));
            }
        }
        catch (Exception ex) {
            Debug.Log(ex.Message);
        }
        finally {
            streamWriter.Close();
        }
    }

    [Serializable]
    public class ReplayFrame {
        public Vector3 pos;
        public Vector3 rot;
        public float steerAngle;
        public float time;

        public ReplayFrame(Vector3 pos, Vector3 rot, float steerAngle, float time) {
            this.pos = pos;
            this.rot = rot;
            this.steerAngle = steerAngle;
            this.time = time;
        }
    }
}