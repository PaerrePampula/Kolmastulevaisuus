using UnityEngine;
using System.Collections;
using System;

public interface IStattable
{
    T getValue<T>();
    Type GetType();
    StatType ThisStatType { get; }
    bool UniqueStat { get; }
}
