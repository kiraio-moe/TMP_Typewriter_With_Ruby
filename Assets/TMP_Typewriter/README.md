# TMP_Typewriter (With_Ruby)

TMP_Typewriter is a utility to create typing effect for TextMeshPro by utilizing [DOTween](http://dotween.demigiant.com "DOTween") functionality. Seamlessly support Ruby (Furigana) thanks to [RubyTextMeshPro](https://github.com/jp-netsis/RubyTextMeshPro "RubyTextMeshPro").

[![](https://img.shields.io/github/release/kiraio-moe/TMP_Typewriter_With_Ruby.svg?label=latest%20version)](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases)
[![](https://img.shields.io/github/release-date/kiraio-moe/TMP_Typewriter_With_Ruby.svg)](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases)
![](https://img.shields.io/badge/Unity-2020.48f1%2B-white.svg)
![](https://img.shields.io/badge/.NET-4.0%2B-purple.svg)
[![](https://img.shields.io/github/license/kiraio-moe/TMP_Typewriter_With_Ruby.svg)](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/blob/main/LICENSE)

## Features

- Support Rich Text tags.
- Skippable.
- Can pause and resume.
- Can delay typing effect.
- OnComplete callback.
- Compatible with [CharTweener](https://github.com/mdechatech/CharTweener "CharTweener").

## How to Use

- Download DOTween from [DOTween official website](http://dotween.demigiant.com/download.php "DOTween official website") then import.
- Install the modified RubyTextMeshPro via UPM Git URL:

  ```txt
  https://github.com/kiraio-moe/RubyTextMeshPro.git?path=Assets/RubyTextMeshPro
  ```

  or `.unitypackage` from [RubyTextMeshPro Releases](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases "RubyTextMeshPro Releases").

- Install TMP_Typewriter_With_Ruby via UPM Git URL:

  ```txt
  https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby.git?path=Assets/TMP_Typewriter
  ```

  or the classic way with `.unitypackage` from [Releases](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases "Releases").  
  If you install using `.unitypackage`, don't forget to install TextMeshPro via Package Manager.

- Add TMP_Typewriter component to the empty GameObject or that already contain TextMeshPro component.
- Add TMP_Typewriter namespace to your script and you're ready to go.

  ```cs
  using KoganeUnityLib;
  ```

### Examples Usage

#### Normal

<img src="https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-Normal.gif" width="45%" /><br/>

```cs
m_Typewriter.Play
(
    text        : "ABCDEFG HIJKLMN OPQRSTU",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Normal Complete!")
);
```

#### Rich Text

<img src="https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-RichText.gif" width="45%" /><br/>

```cs
m_Typewriter.Play
(
    text        : @"<size=64>ABCDEFG</size> <color=red>HIJKLMN</color> <sprite=0> <link=""https://www.google.com/"">OPQRSTU</link>",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Rich Text Complete!")
);
```

#### Ruby

![](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-Ruby.gif)

You can use `<r>` or `<ruby>` tag to wrap the Furigana (small word on top of a word).

```cs
m_Typewriter.Play
(
    text        : "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Ruby (Furigana) Complete!")
);
```

#### Sprite

<img src="https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-Sprite.gif" width="45%" /><br/>

```cs
m_Typewriter.Play
(
    text        : @"<sprite=0><sprite=0><sprite=1><sprite=2><sprite=3><sprite=4><sprite=5><sprite=6><sprite=7><sprite=8><sprite=9><sprite=10>",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Sprite Complete!")
);
```

#### Delay

![](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-Delay.gif)

The delay duration is in seconds.

```cs
m_Typewriter.Play
(
    text        : "Hello <delay=2>World!</delay>",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Delay Complete!")
);
```

#### Pause & Resume

<img src="https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-Pause_Resume.gif" width="45%" /><br/>

```cs
m_Typewriter.Pause();
m_Typewriter.Resume();
```

#### Skip

<img src="https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-Skip.gif" width="45%" /><br/>

```cs
m_Typewriter.Skip(); // with callback (onComplete invoked)
m_Typewriter.Skip(false); // no callback
```

#### With CharTweener

<img src="https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/raw/main/Promotional/TMP_Typewriter-Demo-WithCharTweener.gif" width="45%" /><br/>

## License

This project is licensed under [MIT License](/LICENSE "Read LICENSE file").

## Credits

- [DOTWeen](http://dotween.demigiant.com "DOTween")
- [RubyTextMeshPro](https://github.com/jp-netsis/RubyTextMeshPro "RubyTextMeshPro")
