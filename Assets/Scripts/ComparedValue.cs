using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CompareValueTypes
{
    StringType,
    FloatType,
    IntType,
    GameObjectType
}
public class ComparedValue<T>
{
    public T Value;
    public CompareValueTypes compareValueType;
}