using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Command")]
public class BaseCommand : ScriptableObject {
    public enum CommandType {
        Heal,
        Buff,
        Attack
    }
    public string commandName;
    public CommandType type;
}