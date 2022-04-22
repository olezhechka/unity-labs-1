using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GrindBalanceTableParser : MonoBehaviour
{
    private const string OUTPUT_FOLDER_ROOT_PATH = "Assets";
    private const string OUTPUT_FILE_EXTENSION = ".asset";

    [MultiLineProperty(20)]
    [LabelText("Який JSON парсити")]
    [InfoBox("Сюди треба вводити серіалізований рядок масиву GameBalanceModels")]
    public string jsonToParse =
    "[\n" +
    "  {\n" +
    "    \"hoMode\":0,\n" +
    "    \"rank\":1,\n" +
    "    \"localEventGrindAward\":[\n" +
    "      {\n" +
    "          \"id\":\"Test 1\",\n" +
    "          \"count\":2.0\n" +
    "      }\n" +
    "    ]\n" +
    "  },\n" +
    "  {\n" +
    "    \"hoMode\":2,\n" +
    "    \"rank\":2,\n" +
    "    \"localEventGrindAward\":[\n" +
    "      {\n" +
    "          \"id\":\"Test 2\",\n" +
    "          \"count\":3.0\n" +
    "      }\n" +
    "    ]\n" +
    "  }\n" +
    "]";
    private bool HasJsonToParse => !string.IsNullOrWhiteSpace(jsonToParse);

    [FolderPath(ParentFolder = OUTPUT_FOLDER_ROOT_PATH, RequireExistingPath = true, UseBackslashes = true)]
    [LabelText("Шлях до вихідної папки")]
    [InfoBox("Шлях береться відносно папки \"" + OUTPUT_FOLDER_ROOT_PATH + "\"")]
    public string outputFolderPath;

    [InfoBox("Сюди треба вводити лише ім'я файлу, без розширення")]
    [LabelText("Назва вихідного файлу")]
    public string outputFileName = "New Grind Balance Table";
    private bool IsValidOutputFileName => !string.IsNullOrWhiteSpace(this.outputFileName);

    [Button(Name = "Спарсити")]
    [DisableIf("@this.ShouldDisableParseButton")]
    [InfoBox("Натиском цієї кнопки ви створите спаршений екземпляр ScriptableObject GrindBalanceTable по вказаному шляху")]
    private void OnParseGrindBalanceTableClick()
    {
        this.ParseGrindBalanceTable(this.jsonToParse);
    }
    private bool ShouldDisableParseButton => !this.HasJsonToParse || !this.IsValidOutputFileName;

    private void ParseGrindBalanceTable(string jsonText)
    {
        GrindBalanceTable newGrindBalanceTable = ScriptableObject.CreateInstance<GrindBalanceTable>();
        newGrindBalanceTable.grindBalanceData = JsonConvert.DeserializeObject<GrindBalanceModel[]>(jsonText);

        string savePath = Path
            .Combine(OUTPUT_FOLDER_ROOT_PATH, this.outputFolderPath, this.outputFileName)
            .Replace('/', '\\');
        savePath += OUTPUT_FILE_EXTENSION;
        AssetDatabase.CreateAsset(newGrindBalanceTable, savePath);
        AssetDatabase.SaveAssets();
    }
}
