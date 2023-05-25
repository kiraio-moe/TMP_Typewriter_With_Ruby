using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace KoganeUnityLib
{
    /// <summary>
    /// TextMesh Pro で 1 文字ずつ表示する演出を再生するコンポーネント
    /// A component that plays a character-by-character rendition in TextMesh Pro
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("TMPro/TMP_Typewriter")]
    public partial class TMP_Typewriter : MonoBehaviour
    {
        //==============================================================================
        // 変数
        // variables
        //==============================================================================
        // Get access to the TMP, so it can be easily used later.
        public RubyTextMeshPro rubyTMP { get; private set; } // TMP with Mesh Renderer
        public RubyTextMeshProUGUI rubyTMP_UGUI { get; private set; } // TMP with Canvas Renderer
        public bool isTyping
        {
            get
            {
                return typingTween.IsPlaying();
            }
            private set { }
        }
        private Action onCompleteCallback;
        private Tween typingTween, delayTween;
        private List<(string text, float delay)> delayTexts =
            new List<(string text, float delay)>();
        private string parsedText;
        private int delayTagCount = 0;
        private float delayTimer;
        // private List<(int index, int count)> m_rubyInfos = new List<(int index, int count)>(32);

        //==============================================================================
        // 関数
        // functions
        //==============================================================================
#if UNITY_EDITOR
        /// <summary>
        /// アタッチされた時や Reset された時に呼び出されます
        /// Called when attached or reset
        /// </summary>
        private void Reset()
        {
            InitializeRubyTMP();
        }

        private void OnValidate()
        {
            InitializeRubyTMP();
            ReplaceDelayTag();
        }
#endif

        /// <summary>
        /// 破棄される時に呼び出されます
        /// Called when destroyed
        /// </summary>
        private void OnDestroy()
        {
            KillTween();
            onCompleteCallback = null;
        }

        /// <summary>
        /// Called early when the game begin.
        /// </summary>
        private void Awake()
        {
            InitializeRubyTMP();
        }

        /// <summary>
        /// 演出を再生します
        /// </summary>
        /// <param name="text">表示するテキスト ( リッチテキスト対応 )</param>
        /// <param name="speed">表示する速さ ( speed == 1 の場合 1 文字の表示に 1 秒、speed == 2 の場合 0.5 秒かかる )</param>
        /// <param name="onComplete">演出完了時に呼び出されるコールバック</param>
        public void Play(string text, float speed = 10f, Action onComplete = null)
        {
            SetUneditedText(text);
            onCompleteCallback = onComplete;

            PopulateDelayTags();
            ReplaceDelayTag();

            ForceMeshUpdate();

            parsedText = GetParsedText();
            int length = parsedText.Length;
            float duration = 1f / speed * length;

            OnUpdate(0);
            KillTween();
            typingTween = DOTween
                .To(value => OnUpdate(value), 0, 1, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => OnCompleteCallback());
        }

        /*
        /// <summary>
        /// 演出を再生します（ルビ対応版）
        /// Play the production (ruby version)
        /// </summary>
        /// <param name="text">表示するテキスト ( リッチテキスト対応 ) | Text to display (supports rich text)</param>
        /// <param name="speed">表示する速さ ( speed == 1 の場合 1 文字の表示に 1 秒、speed == 2 の場合 0.5 秒かかる ) | Display speed (it takes 1 second to display 1 character if speed == 1, 0.5 seconds if speed == 2)</param>
        /// <param name="onComplete">演出完了時に呼び出されるコールバック | A callback that will be called when the rendering is complete</param>
        /// <param name="fixedLineHeight">ルビ表示用に行間を固定する | Fix line spacing for ruby display</param>
        /// <param name="autoMarginTop">1行目にルビがある時はMarginTopで位置調整する | Fix line spacing for ruby display</param>
        public void Play( string text, float speed, Action onComplete, bool fixedLineHeight = false, bool autoMarginTop = true)
        {
            rubyTMP.text = text;
            rubyTMP.ForceMeshUpdate();
            onCompleteCallback = onComplete;

            // ルビタグ展開前のリッチテキスト除外テキストを取得
            // Get rich text exclusion text before ruby tag expansion
            parsedText = rubyTMP.GetParsedText();
            SetRubyInfos(parsedText);

            parsedText = TMProRubyUtil.RemoveRubyTag(parsedText);
            var length = parsedText.Length;
            var duration = 1 / speed * length;

            rubyTMP.SetTextAndExpandRuby(text, fixedLineHeight, autoMarginTop);
            rubyTMP.ForceMeshUpdate();

            OnUpdate( 0 );

            if ( typingTween != null )
            {
                typingTween.Kill();
            }

            typingTween = DOTween
                .To( value => OnUpdate( value ), 0, 1, duration )
                .SetEase( Ease.Linear )
                .OnComplete( () => OnComplete() )
            ;
        }
        */

        /*
        /// <summary>
        /// ルビタグごとの漢字の終了位置（ルビタグを除外した位置）と、ルビの文字数を取得
        /// Get the end position of kanji for each ruby tag (position excluding ruby tag) and the number of ruby characters
        /// </summary>
        public void SetRubyInfos(string text)
        {
            m_rubyInfos.Clear();
            var match = TMProRubyUtil.TagRegex.Match(text);
            while (match.Success)
            {
                if (match.Groups.Count > 2)
                {
                    var ruby = match.Groups["ruby"];
                    var kanji = match.Groups["kanji"];
                    m_rubyInfos.Add((ruby.Index + kanji.Value.Length - 3, ruby.Value.Length));
                    text = text.Replace(match.Groups[0].Value, kanji.Value);
                    match = TMProRubyUtil.TagRegex.Match(text);
                }
                else
                {
                    match.NextMatch();
                }
            }
        }
        */

        /// <summary>
        /// 演出をスキップします
        /// </summary>
        /// <param name="withCallbacks">演出完了時に呼び出されるコールバックを実行する場合 true</param>
        public void Skip(bool withCallbacks = true)
        {
            KillTween();
            OnUpdate(1);

            if (!withCallbacks)
                return;

            InvokeOnCompleteCallback();
        }

        /// <summary>
        /// 演出を一時停止します
        /// </summary>
        public void Pause()
        {
            typingTween?.Pause();
        }

        /// <summary>
        /// 演出を再開します
        /// </summary>
        public void Resume()
        {
            typingTween?.Play();
        }

        /// <summary>
        /// 演出を更新する時に呼び出されます
        /// Called when rendition is updated
        /// </summary>
        private void OnUpdate(float value)
        {
            float current = Mathf.Lerp(0, parsedText.Length, value);
            int count = Mathf.FloorToInt(current);
            // var rubyAddedCount = count;

            // foreach (var info in m_rubyInfos)
            // {
            // 	if (count >= info.index)
            // 	{
            // 		rubyAddedCount += info.count;
            // 	}
            // }

            // rubyTMP.maxVisibleCharacters = rubyAddedCount;

            SetMaxVisibleCharacters(count);

            if (delayTexts.Count > 0)
            {
                string delayTagKey = delayTexts[delayTagCount].text;
                float delayTagValue = delayTexts[delayTagCount].delay;
                string delayContent = "";

                if ((count + delayTagKey.Length) <= parsedText.Length)
                    delayContent = parsedText.Substring(count, delayTagKey.Length);

                if (delayContent.Contains(delayTagKey))
                {
                    Pause();

                    // NOTE: Don't know why this function is get executed twice.
                    // It's affect the delay duration to be doubled.
                    // To tackle this, we divide it by 2.
                    delayTween = DOTween
                        .To(() => delayTimer, x => delayTimer = x, 1, delayTagValue / 2f)
                        .OnComplete(() => {
                            if (delayTagCount < delayTexts.Count - 1)
                            {
                                delayTagCount++;
                                delayTimer = 0;
                            }
                            else
                            {
                                delayTagCount = 0;
                                delayTimer = 0;
                            }

                            Resume();
                        });
                }
            }
        }

        /// <summary>
        /// 演出が更新した時に呼び出されます
        /// </summary>
        private void OnCompleteCallback()
        {
            typingTween = null;
            InvokeOnCompleteCallback();
        }

        /// <summary>
        /// Get RubyTMP that already exist or create a new one.
        /// </summary>
        private void InitializeRubyTMP()
        {
            Canvas canvas = GetComponentInParent<Canvas>();

            if (canvas == null)
            {
                rubyTMP = GetComponent<RubyTextMeshPro>();
                if (rubyTMP == null)
                    rubyTMP = gameObject.AddComponent<RubyTextMeshPro>();
            }
            else
            {
                rubyTMP_UGUI = GetComponent<RubyTextMeshProUGUI>();
                if (rubyTMP_UGUI == null)
                    rubyTMP_UGUI = gameObject.AddComponent<RubyTextMeshProUGUI>();
            }
        }

        /// <summary>
        /// Set RubyTMP unedited (tags still intact) text content.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Unedited text.</returns>
        private void SetUneditedText(string text)
        {
            if (rubyTMP != null)
                rubyTMP.uneditedText = text;
            if (rubyTMP_UGUI != null)
                rubyTMP_UGUI.uneditedText = text;
        }

        /// <summary>
        /// Force RubyTMP to update their Mesh data.
        /// </summary>
        private void ForceMeshUpdate()
        {
            if (rubyTMP != null)
                rubyTMP.ForceMeshUpdate();
            if (rubyTMP_UGUI != null)
                rubyTMP_UGUI.ForceMeshUpdate();
        }

        /// <summary>
        /// Get parsed text (removed tags) of RubyTMP.
        /// </summary>
        /// <returns>Parsed text (removed tags).</returns>
        private string GetParsedText()
        {
            if (rubyTMP != null)
                return rubyTMP.GetParsedText();
            if (rubyTMP_UGUI != null)
                return rubyTMP_UGUI.GetParsedText();
            return string.Empty;
        }

        /// <summary>
        /// Kill the Tweening.
        /// </summary>
        private void KillTween()
        {
            typingTween?.Kill();
            typingTween = null;

            delayTween?.Kill();
            delayTween = null;
            delayTimer = 0;
        }

        /// <summary>
        /// Set max RubyTMP characters that appear in screen.
        /// </summary>
        /// <param name="count"></param>
        private void SetMaxVisibleCharacters(int count)
        {
            if (rubyTMP != null)
                rubyTMP.maxVisibleCharacters = count;
            if (rubyTMP_UGUI != null)
                rubyTMP_UGUI.maxVisibleCharacters = count;
        }

        /// <summary>
        /// Invoke OnCompleteCallback().
        /// </summary>
        private void InvokeOnCompleteCallback()
        {
            onCompleteCallback?.Invoke();
            onCompleteCallback = null;
        }

        private void PopulateDelayTags()
        {
            delayTexts.Clear();
            delayTagCount = 0;

            string uneditedText = "";
            string pattern = @"<delay=(\d+)>(.*?)<\/delay>";

            if (rubyTMP != null)
                uneditedText = rubyTMP.uneditedText;
            if (rubyTMP_UGUI != null)
                uneditedText = rubyTMP_UGUI.uneditedText;

            MatchCollection matches = Regex.Matches(uneditedText, pattern);

            foreach (Match match in matches)
            {
                string delayValue = match.Groups[1].Value;
                string tagContent = match.Groups[2].Value;

                delayTexts.Add((tagContent, float.Parse(delayValue)));
            }
        }

        /// <summary>
        /// Remove the '<delay=x></delay>' string from screen, so it's only show the tag content.
        /// NOTE: Don't know why this method didn't always get executed in OnValidate or OnBecameVisible().
        /// </summary>
        private void ReplaceDelayTag()
        {
            // Remove <delay> tags from the tag content
            string parsedText = "";
            string pattern = @"<delay=\d+>(.*?)<\/delay>";

            if (rubyTMP != null)
            {
                parsedText = Regex.Replace(rubyTMP.text, pattern, "$1");
                rubyTMP.text = parsedText;

                if (rubyTMP.havePropertiesChanged)
                    rubyTMP.ForceMeshUpdate();
            }

            if (rubyTMP_UGUI != null)
            {
                parsedText = Regex.Replace(rubyTMP_UGUI.text, pattern, "$1");
                rubyTMP_UGUI.text = parsedText;

                if (rubyTMP_UGUI.havePropertiesChanged)
                    rubyTMP_UGUI.ForceMeshUpdate();
            }
        }
    }
}
