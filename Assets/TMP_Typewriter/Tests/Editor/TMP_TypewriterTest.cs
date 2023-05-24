using DG.Tweening;
using NUnit.Framework;
using UnityEngine;

namespace KoganeUnityLib.Tests
{
    public class TMP_TypewriterTest
    {
        TMP_Typewriter typewriter;
        float waitTime;

        [SetUp]
        public void Setup()
        {
            typewriter = new GameObject(
                "TMP_Typewriter",
                typeof(TMP_Typewriter)
            ).AddComponent<TMP_Typewriter>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(typewriter.gameObject);
        }

        [Test]
        public void Play()
        {
            typewriter.Play(
                text: "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed: 10,
                onComplete: () => Assert.True(true)
            );
        }

        [Test]
        public void SkipWithCallback()
        {
            typewriter.Play(
                text: "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed: 10,
                onComplete: () => Assert.True(true) // <- Callback
            );

            // DOTween
            //     .To(() => waitTime, x => waitTime = x, 1, 1.5f)
            //     .OnComplete(() => typewriter.Skip(true));

            DOVirtual.DelayedCall(1.5f, () => typewriter.Skip(true));
        }

        [Test]
        public void SkipNoCallback()
        {
            bool callbackCalled = false;

            typewriter.Play(
                text: "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed: 10,
                onComplete: () => Assert.False(callbackCalled)
            );

            // DOTween
            //     .To(() => waitTime, x => waitTime = x, 1, 1.5f)
            //     .OnComplete(() => typewriter.Skip(callbackCalled = false));

            DOVirtual.DelayedCall(1.5f, () => typewriter.Skip(callbackCalled = false));
        }

        [Test]
        public void Pause()
        {
            typewriter.Play(
                text: "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed: 10,
                onComplete: () => Assert.False(true)
            );

            // DOTween
            //     .To(() => waitTime, x => waitTime = x, 1, 1.5f)
            //     .OnComplete(() =>
            //     {
            //         Assert.True(true);
            //         typewriter.Pause();
            //     });

            DOVirtual.DelayedCall(1.5f, () => {
                Assert.True(true);
                typewriter.Pause();
            });
        }

        [Test]
        public void Resume()
        {
            typewriter.Play(
                text: "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed: 10,
                onComplete: () => Assert.True(true)
            );

            DOTween
                .Sequence()
                .Append(
                    DOVirtual.DelayedCall(1.5f, () => typewriter.Pause())
                )
                // .Append(
                //     DOTween
                //         .To(() => waitTime, x => waitTime = x, 1, 1.5f)
                //         .OnComplete(() => typewriter.Pause())
                // )
                .Append(DOVirtual.DelayedCall(1, () => typewriter.Resume()));
                // .Append(
                //     DOTween
                //         .To(() => waitTime, x => waitTime = x, 1, 1)
                //         .OnComplete(() => typewriter.Resume())
                // );
        }
    }
}
