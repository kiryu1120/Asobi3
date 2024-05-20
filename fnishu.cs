using UnityEngine;

public class Finish : MonoBehaviour
{
    // ゲームを終了するための関数
    public void QuitGame()
    {
#if UNITY_EDITOR
        // エディターでプレイ中の場合、ゲームを終了しないようにする
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルドしたアプリケーションの場合、アプリケーションを終了する
        Application.Quit();
#endif
    }
}