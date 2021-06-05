# 🔧 TOME: TSV To JSON Converter
Small dotnet core console app for converting script TSV documents into JSON format

`dotnet run path/file.tsv`

## Input Document
Document should be a TOME-format tabs-separated values (TSV) document.
An example of such document can be found there: [TOME Example](https://docs.google.com/spreadsheets/d/1_-pYQTF3__aO8ktGBoTXIJRwPD7H56V2t4YUsSewUGw/edit#gid=0)

A document should have following columns:

| Chapter | Character | Text            | Condition     | Action   |
| ------- | --------- | --------------- | ------------- | -------- |
| -       | -         | -               | -             | -        |

Document can also have additional columns, configuration of the document can be changed by changing
- TSV/Data/Line data model
- TSV/Parser/LineParser parser instructions
