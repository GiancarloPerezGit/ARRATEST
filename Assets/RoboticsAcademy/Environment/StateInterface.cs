using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StateChecker
{
    bool returnState();
    void execute();
    void setOrder(int ord);
    void exitState();
}
