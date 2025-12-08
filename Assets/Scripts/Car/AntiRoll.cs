using System;
using System.Collections.Generic;
using UnityEngine;

public class AntiRoll : MonoBehaviour {
    float antiRoll = 5000f;
    Rigidbody bodyRb;
    
    List<Tuple<Suspension, Suspension>> suspensionPairs;

    public void Initialize(float antiRoll, params Suspension[] suspensions) {
        bodyRb = GetComponent<Rigidbody>();

        this.antiRoll = antiRoll;
        
        for (int i = 0; i < suspensions.Length; i) {
            if (i > suspensions.Length - 1) {
                break;
            }
            
            suspensionPairs.Add(new Tuple<Suspension, Suspension>(suspensions[i], suspensions[i + 1]));
        }
    }
    
    void FixedUpdate() {
        if (bodyRb == null) {
            return;
        }
        
        StabilizerBars();
    }

    void StabilizerBars() {
        foreach (var suspensionPair in suspensionPairs) {
            Suspension left = suspensionPair.Item1;
            Suspension right = suspensionPair.Item2;
            
            float lastRightCompression = 1F;
        
            if (right.grounded) {
                lastRightCompression = right.lastCompression;
            }
        
            float lastLeftCompression = 1F;
        
            if (left.grounded) {
                lastLeftCompression = left.lastCompression;
            }
        
            var compressionDiff = (lastLeftCompression - lastRightCompression) * antiRoll;
            if (right.grounded) {
                bodyRb.AddForceAtPosition(right.transform.up * -compressionDiff, right.transform.position);
            }
        
            if (left.grounded) {
                bodyRb.AddForceAtPosition(left.transform.up * compressionDiff, left.transform.position);
            }
        }
    }
}