# TMP_Typewriter (With_Ruby)

TMP_Typewriter is a utility to create typing effect for TextMeshPro by utilizing [DOTween](http://dotween.demigiant.com "DOTween") funtionality. Support Ruby (Furigana) to write Kanji thanks to [RubyTextMeshPro](https://github.com/jp-netsis/RubyTextMeshPro "RubyTextMeshPro") by [jp-netsis](https://github.com/jp-netsis "jp-netsis").

[![](https://img.shields.io/github/release/kiraio-moe/TMP_Typewriter_With_Ruby.svg?label=latest%20version)](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases)
[![](https://img.shields.io/github/release-date/kiraio-moe/TMP_Typewriter_With_Ruby.svg)](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases)
![](https://img.shields.io/badge/Unity-2020.48f1%2B-red.svg)
![](https://img.shields.io/badge/.NET-4.0%2B-orange.svg)
[![](https://img.shields.io/github/license/kiraio-moe/TMP_Typewriter_With_Ruby.svg)](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/blob/main/LICENSE)


## Features

- Support Rich Text tags.
- Skippable.
- Can pause and resume.
- Can delay typing effect.
- OnComplete callback.
- Compatible with [CharTweener](https://github.com/mdechatech/CharTweener "CharTweener").

## How to Use

- Download TMP_Typewriter_With_Ruby `.unitypackage` from [Releases](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases "Releases") then import or install through Package Manager git URL:

```url
https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby.git?path=Assets/TMP_Typewriter
```

- Download DOTween from the [official website](http://dotween.demigiant.com/download.php "DOTween official website") and import.
- Download modified RubyTextMeshPro from [Releases](https://github.com/kiraio-moe/TMP_Typewriter_With_Ruby/releases "Releases")
- Add TMP_Typewriter component to the empty GameObject or that already contain TextMeshPro component.
- Add TMP_Typewriter_With_Ruby namespace to your script and you're ready to go.

```
using KoganeUnityLib;
```

### Examples Usage

#### Normal

![](./Promotional/TMP_Typewriter-Demo-Normal.gif)

```cs
m_Typewriter.Play
(
    text        : "ABCDEFG HIJKLMN OPQRSTU",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Normal Complete!")
);
```

#### Rich Text

![](./Promotional/TMP_Typewriter-Demo-RichText.gif)

```cs
m_Typewriter.Play
(
    text        : @"<size=64>ABCDEFG</size> <color=red>HIJKLMN</color> <sprite=0> <link=""https://www.google.co.jp/"">OPQRSTU</link>",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Rich Text Complete!")
);
```

#### Ruby

![](./Promotional/TMP_Typewriter-Demo-Ruby.gif)

You can use <r> or <ruby> tag to wrap the Furigana.

```cs
m_Typewriter.Play
(
    text        : "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Ruby (Furigana) Complete!")
);
```

#### Sprite

![](./Promotional/TMP_Typewriter-Demo-Sprite.gif)

```cs
m_Typewriter.Play
(
    text        : @"<sprite=0><sprite=0><sprite=1><sprite=2><sprite=3><sprite=4><sprite=5><sprite=6><sprite=7><sprite=8><sprite=9><sprite=10>",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Sprite Complete!")
);
```

#### Delay

![](./Promotional/TMP_Typewriter-Demo-Delay.gif)

```cs
m_Typewriter.Play
(
    text        : "Hello <delay=2>World!</delay>",
    speed       : m_TypingSpeed,
    onComplete  : () => Debug.Log("Typing Delay Complete!")
);
```

#### Pause & Resume

![](./Promotional/TMP_Typewriter-Demo-Pause_Resume.gif)

```cs
m_Typewriter.Pause();
m_Typewriter.Resume();
```

#### Skip

![](./Promotional/TMP_Typewriter-Demo-Skip.gif)

```cs
m_Typewriter.Skip(); // with callback (onComplete invoked)
m_Typewriter.Skip(false); // no callback
```

#### Other

![](./Promotional/TMP_Typewriter-Demo-WithCharTweener.gif)

Compatible with [CharTweener](https://github.com/mdechatech/CharTweener "CharTweener").

## License

This project is licensed under [MIT License](/LICENSE "Read LICENSE file").

## Credits

- [ina-amagami](https://github.com/ina-amagami "ina-amagami") for the original idea.
- [DOTWeen](http://dotween.demigiant.com "DOTween")
- [RubyTextMeshPro](https://github.com/jp-netsis/RubyTextMeshPro "RubyTextMeshPro")
