using System.Collections.Generic;
using System;

[Serializable]
public class LocalizationData
{
    public Dictionary<string, string> localizedTexts;
    public Dictionary<string, Dictionary<string, string>> narrationsTexts;
}