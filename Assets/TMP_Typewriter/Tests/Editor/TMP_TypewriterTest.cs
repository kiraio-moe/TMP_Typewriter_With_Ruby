using DG.Tweening;
using NUnit.Framework;
using UnityEngine;

namespace KoganeUnityLib.Tests
{
    public class TMP_TypewriterTest
    {
        TMP_Typewriter typewriter;

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
                text: "Hello World!",
                speed: 10,
                onComplete: () => Assert.True(true)
            );
        }

        [Test]
        public void SkipWithCallback()
        {
            typewriter.Play(
                text: "Hello World!",
                speed: 10,
                onComplete: () => Assert.True(true)
            );

            DOVirtual.DelayedCall(1.5f, () => typewriter.Skip(true));
        }

        [Test]
        public void SkipNoCallback()
        {
            bool callbackCalled = false;

            typewriter.Play(
                text: "Hello World!",
                speed: 10,
                onComplete: () => Assert.False(callbackCalled)
            );

            DOVirtual.DelayedCall(1.5f, () => typewriter.Skip(callbackCalled = false));
        }

        [Test]
        public void Pause()
        {
            typewriter.Play(
                text: "Hello World!",
                speed: 10,
                onComplete: () => Assert.False(true)
            );

            DOVirtual.DelayedCall(1.5f, () => {
                typewriter.Pause();
                Assert.True(true);
            });
        }

        [Test]
        public void Resume()
        {
            typewriter.Play(
                text: "Hello World!",
                speed: 10,
                onComplete: () => Assert.True(true)
            );

            DOTween
                .Sequence()
                .Append(DOVirtual.DelayedCall(1.5f, () => typewriter.Pause()))
                .Append(DOVirtual.DelayedCall(1, () => typewriter.Resume()));
        }
    }
}
