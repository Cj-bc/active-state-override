using System;
using UnityEngine;

#nullable enable

public class ActiveStateOverride : MonoBehaviour
{
    public struct OverrideHandler
    {
        public OverrideHandler(Action activate, Action deactivate, Action _return)
        {
            m_Activate = activate;
            m_Deactivate = deactivate;
            m_Return = _return;
        }

        private readonly Action m_Activate;
        private readonly Action m_Deactivate;
        private readonly Action m_Return;

        public void Activate() => m_Activate();
        public void Deactivate() => m_Deactivate();
        public void Return() => m_Return();

        public void SetActive(bool state)
        {
            if (state)
            {
                Activate();
            } else
            {
                Deactivate();
            }
        }
    }

    private UnityEngine.Object? OverridenBy = null;

    public OverrideHandler? Override(UnityEngine.Object overridenBy)
    {
        if (OverridenBy != null) return null;
        OverridenBy = overridenBy;
        return new OverrideHandler(Activate, Deactivate, StopOverride);
    }

    private void StopOverride()
    {
        OverridenBy = null;
    }

    private void Activate() => gameObject.SetActive(true);
    private void Deactivate() => gameObject.SetActive(false);

}
