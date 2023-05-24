using UnityEngine;

namespace KoganeUnityLib.Example
{
	public class Example : MonoBehaviour
	{
		[SerializeField] TMP_Typewriter[] m_typewriters;
		[SerializeField] float m_speed = 10f;

		void Update()
		{
            foreach (TMP_Typewriter typewriter in m_typewriters)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    typewriter.Play
                    (
                        text        : "Ah, <delay=2>hmm...</delay>\nMmm... <delay=4>meeoww...</delay>",
                        speed       : m_speed,
                        onComplete  : () => Debug.Log( "完了" )
                    );
                }

                if ( Input.GetKeyDown( KeyCode.Z ) )
                {
                    // 1 文字ずつ表示する演出を再生（ルビ対応）
                    typewriter.Play
                    (
                        text        : "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                        speed       : m_speed,
                        onComplete  : () => Debug.Log( "完了" )
                    );
                }

                if ( Input.GetKeyDown( KeyCode.X ) )
                {
                    // 1 文字ずつ表示する演出を再生（リッチテキスト対応）
                    typewriter.Play
                    (
                        text        : @"<size=64>ABCDEFG</size> <color=red>HIJKLMN</color> <sprite=0> <link=""https://www.google.co.jp/"">OPQRSTU</link>",
                        speed       : m_speed,
                        onComplete  : () => Debug.Log( "完了" )
                    );
                }

                if ( Input.GetKeyDown( KeyCode.C ) )
                {
                    // 1 文字ずつ表示する演出を再生（スプライト対応）
                    typewriter.Play
                    (
                        text        : @"<sprite=0><sprite=0><sprite=1><sprite=2><sprite=3><sprite=4><sprite=5><sprite=6><sprite=7><sprite=8><sprite=9><sprite=10>",
                        speed       : m_speed,
                        onComplete  : () => Debug.Log( "完了" )
                    );
                }

                if ( Input.GetKeyDown( KeyCode.V ) )
                {
                    // 演出をスキップ（onComplete は呼び出される）
                    typewriter.Skip();
                }

                if ( Input.GetKeyDown( KeyCode.B ) )
                {
                    // 演出をスキップ（onComplete は呼び出されない）
                    typewriter.Skip( false );
                }

                if ( Input.GetKeyDown( KeyCode.N ) )
                {
                    // 演出を一時停止
                    typewriter.Pause();
                }

                if ( Input.GetKeyDown( KeyCode.M ) )
                {
                    // 演出を再開
                    typewriter.Resume();
                }
            }
		}
	}
}
