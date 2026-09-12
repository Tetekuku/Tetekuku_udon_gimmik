using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Tetekuku
{
    /// <summary>
    /// 街灯の光の落ち（light_fall）を、プレイヤーが近づいたときだけ表示する。
    /// 見た目だけの演出であり、各プレイヤーが自分と街灯の距離だけで判定するため同期しない。
    /// </summary>
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    [AddComponentMenu("Tetekuku/Street Light")]
    public class StreetLight : UdonSharpBehaviour
    {
        [Header("対象設定")]
        [SerializeField, Tooltip("プレイヤーが近づいたときに表示する、光の落ちの GameObject（light_fall）。")]
        private GameObject lightFall;

        [Header("判定設定")]
        [SerializeField, Tooltip("この距離（メートル）以内にプレイヤーがいる間だけ、光の落ちを表示する。")]
        private float radius = 3f;

        private bool isOn;

        private void Start()
        {
            if (lightFall == null)
            {
                Debug.LogWarning("[StreetLight] Light Fall が設定されていません。インスペクタで対象の GameObject を設定してください。");
                return;
            }

            // 起動直後は未判定のため、最初の Update で正しい状態に補正されるまで一旦消しておく。
            isOn = false;
            lightFall.SetActive(false);
        }

        private void Update()
        {
            if (lightFall == null) return;

            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer == null) return;

            float distance = Vector3.Distance(transform.position, localPlayer.GetPosition());
            bool shouldBeOn = distance <= radius;

            if (shouldBeOn == isOn) return;

            isOn = shouldBeOn;
            lightFall.SetActive(isOn);
        }
    }
}
