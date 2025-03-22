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
            m_IsReturned = false;
        }

        private readonly Action m_Activate;
        private readonly Action m_Deactivate;
        private readonly Action m_Return;
        private bool m_IsReturned;

        public void Activate()
        {
            if (m_IsReturned) return;

            m_Activate();
        }

        public void Deactivate()
        {
            if (m_IsReturned) return;

            m_Deactivate();
        }

        public void Return()
        {
            if (m_IsReturned) return;

            m_IsReturned = true;
            m_Return();
        }

        public void SetActive(bool state)
        {
            if (m_IsReturned) return;

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
