# Description
A package with a globally unique identifier (GUID) that can be serialized with Unity and used in game scripts. 

### I am NOT the author of the original idea or script. 
I just extended the functionality and extracted it as a separate package. I would like to thank Adam Myhre, aka Git Amend, for creating the script. Please visit his YouTube channel for more tips on Unity game development. https://www.youtube.com/@git-amend

# Getting Started
Just create a serialized field of SerializableGuid type and it will be shown in the inspector.
```csharp
[SerializedField] private SerializableGuid _guid;
```
To initialize the field, you can use the “Regenerate GUID” option in the context menu in the inspector.
![image](https://github.com/user-attachments/assets/6b8a926f-a4a1-4fcf-86b9-ae7f47b456fb)

You can also use the NewGuid() method in the script.
```csharp
[SerializedField] private SerializableGuid _guid = SerializableGuid.NewGuid();
```
