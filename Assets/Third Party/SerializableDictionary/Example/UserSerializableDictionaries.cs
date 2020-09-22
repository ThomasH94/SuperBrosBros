using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityAtoms;

/// <summary>
/// This is the class that will hold all dicionary defitions we wish to define and serialize
/// Add more here if you need more
/// IMPORTANT! Each new class needs a custom property drawer
/// Script Reference: UserSerializableDictionaryPropertyDrawer via the Editor folder of the Serializable Dictionary Folders
/// </summary>
[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> { }

[Serializable]
 public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> { }

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> { }

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> { }

[Serializable]
public class MyClass
{
    public int i;
    public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> { }

[Serializable]
public class StringAudioClipDictionary : SerializableDictionary<string,AudioClip>{ }

[Serializable]
public class StringVoidEventDictionary : SerializableDictionary<string,VoidEvent>{ }