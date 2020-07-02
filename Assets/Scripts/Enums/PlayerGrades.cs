using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum PlayerGrade
{
    F,
    E,
    D,
    C,
    B,
    A,
    S,
    SS,
    SSS
}
static class Grader
{
    static List<GradeRange> gradeRanges = new List<GradeRange>()
    {
        new GradeRange(){ grade = PlayerGrade.F, ranges = (ConfigFileReader.getValue("GRADEFMIN"), ConfigFileReader.getValue("GRADEFMAX")) },
        new GradeRange(){ grade = PlayerGrade.E, ranges = (ConfigFileReader.getValue("GRADEEMIN"), ConfigFileReader.getValue("GRADEEMAX")) },
        new GradeRange(){ grade = PlayerGrade.D, ranges = (ConfigFileReader.getValue("GRADEDMIN"), ConfigFileReader.getValue("GRADEDMAX")) },
        new GradeRange(){ grade = PlayerGrade.C, ranges = (ConfigFileReader.getValue("GRADECMIN"), ConfigFileReader.getValue("GRADECMAX")) },
        new GradeRange(){ grade = PlayerGrade.B, ranges = (ConfigFileReader.getValue("GRADEBMIN"), ConfigFileReader.getValue("GRADEBMAX")) },
        new GradeRange(){ grade = PlayerGrade.A, ranges = (ConfigFileReader.getValue("GRADEAMIN"), ConfigFileReader.getValue("GRADEAMAX")) },
        new GradeRange(){ grade = PlayerGrade.S, ranges = (ConfigFileReader.getValue("GRADESMIN"), ConfigFileReader.getValue("GRADESMAX")) },
        new GradeRange(){ grade = PlayerGrade.SS, ranges = (ConfigFileReader.getValue("GRADESSMIN"), ConfigFileReader.getValue("GRADESSMAX")) },
        new GradeRange(){ grade = PlayerGrade.SS, ranges = (ConfigFileReader.getValue("GRADESSSMIN"), ConfigFileReader.getValue("GRADESSSMAX")) }

    };
    public static PlayerGrade getGrade(float value)
    {
        Debug.Log(gradeRanges[0].ranges.Item1 + "-" + gradeRanges[0].ranges.Item2);
        var grade = gradeRanges.FirstOrDefault(x => (value < x.ranges.Item2) && (value >= x.ranges.Item1));

        return grade.grade;
    }
}
class GradeRange
{
    public (float, float) ranges;
    public PlayerGrade grade;
}