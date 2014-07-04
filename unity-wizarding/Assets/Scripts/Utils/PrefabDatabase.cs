using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for preab databases. Supposed to be used with an enum as the generic type. Is a singleton. (Maybe change that?)
/// </summary>
[ExecuteInEditMode]
public class PrefabDatabase<KType> : MonoBehaviour where KType : struct, IConvertible  {
	[SerializeField]
	GameObject[] SavedPrefabs;

	public static PrefabDatabase<KType> Instance { get; private set; }

	void Awake() {
		Instance = this;
	}

	public void SetData(KType _key, GameObject _prefab) {
		//Convert the key to an array index. Has to be done that way (WTF C#?)
		int index = (int) (ValueType) _key;

		//Allocate a new spot in the array if the array is for some reason not the correct size.
		if (index >= SavedPrefabs.Length) {
			GameObject[] temp = new GameObject[GetKeyCount()];
			SavedPrefabs.CopyTo(temp, SavedPrefabs.Length);
			SavedPrefabs = temp;
		}

		SavedPrefabs [index] = _prefab;
	}

	public GameObject GetData(KType _key) {
		return SavedPrefabs[(int) (ValueType) _key];
	}

	public int GetKeyCount() {
		return Enum.GetValues (typeof(KType)).Length;
	}


}
