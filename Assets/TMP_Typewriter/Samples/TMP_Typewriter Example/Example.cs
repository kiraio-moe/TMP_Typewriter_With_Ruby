using UnityEngine;

namespace KoganeUnityLib.Example
{
	public class Example : MonoBehaviour
	{
		[SerializeField] TMP_Typewriter m_Typewriter;
		[SerializeField] float m_TypingSpeed = 10f;

        public void PlayNormal()
        {
            m_Typewriter.Play
            (
                text        : "ABCDEFG HIJKLMN OPQRSTU",
                speed       : m_TypingSpeed,
                onComplete  : () => Debug.Log("Typing Normal Complete!")
            );
        }

        public void PlayRichText()
        {
            m_Typewriter.Play
            (
                text        : @"<size=64>ABCDEFG</size> <color=red>HIJKLMN</color> <sprite=0> <link=""https://www.google.co.jp/"">OPQRSTU</link>",
                speed       : m_TypingSpeed,
                onComplete  : () => Debug.Log("Typing Rich Text Complete!")
            );
        }

        public void PlayRuby()
        {
            // You can use <r> or <ruby> to wrap the Furigana.
            m_Typewriter.Play
            (
                text        : "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed       : m_TypingSpeed,
                onComplete  : () => Debug.Log("Typing Ruby (Furigana) Complete!")
            );
        }

        public void PlaySprite()
        {
            m_Typewriter.Play
            (
                text        : @"<sprite=0><sprite=0><sprite=1><sprite=2><sprite=3><sprite=4><sprite=5><sprite=6><sprite=7><sprite=8><sprite=9><sprite=10>",
                speed       : m_TypingSpeed,
                onComplete  : () => Debug.Log("Typing Sprite Complete!")
            );
        }

        public void PlayDelay()
        {
            m_Typewriter.Play
            (
                text        : "Hello <delay=2>World!</delay>",
                speed       : m_TypingSpeed,
                onComplete  : () => Debug.Log("Typing Delay Complete!")
            );
        }

        public void Pause()
        {
            m_Typewriter.Pause();
            Debug.Log("Paused.");
        }

        public void Resume()
        {
            m_Typewriter.Resume();
            Debug.Log("Resume...");
        }

        public void SkipWithCallback()
        {
            m_Typewriter.Skip();
            Debug.Log("Skip with Callback.");
        }

        public void SkipNoCallback()
        {
            m_Typewriter.Skip(false);
            Debug.Log("Skip no Callback.");
        }
	}
}
