This RubyTextMeshPro is slightly customized to fit TMP_Typewriter.
Specificly, `uneditedText` properties in `RubyTextMeshPro` and `RubyTextMeshProUGUI` is edited to return the value of `_uneditedText` variable.

```cs
// Before
public string uneditedText
{
    set
    {
        this._uneditedText = value;
        this.SetTextCustom(this._uneditedText);
    }
}

// After
public string uneditedText
{
    get
    {
        return this._uneditedText;
    }
    set
    {
        this._uneditedText = value;
        this.SetTextCustom(this._uneditedText);
    }
}
```
