using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SkillController : NetworkBehaviour {

    PlayerController m_PlayerController;
    Rigidbody m_rigidbody;
    GameObject SkillAButton;
    GameObject SkillBButton;
    GameObject SkillCButton; 
    GameObject SkillNormalButton;

    Slider slider;

    bool isTriggerSkill;
    Vector3 currentSpeed;
    Text AttributeText;

    SkillEffect skillEffect;

    void Start () {
        Init();
    }

    void Init()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_rigidbody = GetComponent<Rigidbody>();
        skillEffect = GetComponent<SkillEffect>();
        AttributeText = ReferenceManager.instance.PersonInfoPanel.transform.Find("attribute").GetComponent<Text>();
        SkillNormalButton = ReferenceManager.instance.SkillPanel.transform.Find("SkillNormal").gameObject;
        SkillAButton = ReferenceManager.instance.SkillPanel.transform.Find("SkillA").gameObject;
        SkillBButton = ReferenceManager.instance.SkillPanel.transform.Find("SkillB").gameObject;
        SkillCButton = ReferenceManager.instance.SkillPanel.transform.Find("SkillC").gameObject;
        slider = ReferenceManager.instance.EnergyPanel.GetComponentInChildren<Slider>();
        AddEventTriggerListener(SkillNormalButton, EventTriggerType.PointerDown, SkillNormalTrigger);
        AddEventTriggerListener(SkillNormalButton, EventTriggerType.PointerUp,SkillNormalRelease);
        AddEventTriggerListener(SkillAButton, EventTriggerType.PointerDown, SkillATrigger);
        AddEventTriggerListener(SkillBButton, EventTriggerType.PointerDown, SkillBTrigger);
        AddEventTriggerListener(SkillCButton, EventTriggerType.PointerDown, SkillCTrigger);
        SkillNormalRelease(null);
    }

    void AddEventTriggerListener(GameObject obj, EventTriggerType type,UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    void Update()
    {
        currentSpeed = new Vector3(m_rigidbody.velocity.x, 0, m_rigidbody.velocity.z);

        if (isTriggerSkill)
        {
            m_PlayerController.m_PlayerAttributes.SliderAddVaule = -0.4f;
            if (m_rigidbody.velocity == Vector3.zero)
            {
                skillEffect.CmdAddForce(transform.forward * 1000);
            }
            else
            {
                skillEffect.CmdAddForce(currentSpeed.normalized * 1000);
            }
        }
        else
        {
            m_PlayerController.m_PlayerAttributes.SliderAddVaule = 0.1f;
        }

        if (currentSpeed.magnitude > m_PlayerController.m_PlayerAttributes.MaxSpeed)
        {
            m_rigidbody.velocity /= currentSpeed.magnitude / m_PlayerController.m_PlayerAttributes.MaxSpeed;
        }

        if (slider.value == 0)
        {
            SkillNormalRelease(null);
        }

        SliderADD();
    }

    #region 技能触发
    public void SkillNormalTrigger(BaseEventData eventData)
    {
        isTriggerSkill = true;
        m_PlayerController.m_PlayerAttributes.StrengthValue = 10;
        m_PlayerController.m_PlayerAttributes.MaxSpeed = 40;
        m_PlayerController.m_PlayerAttributes.ExpansionMultiple = 3;
        AttributeText.text = "力:" + m_PlayerController.m_PlayerAttributes.PowerValue +
           " 体:" + m_PlayerController.m_PlayerAttributes.StrengthValue +
           " 智:" + m_PlayerController.m_PlayerAttributes.IntelligenceValue;
        skillEffect.UpdateAttribute(true);
    }

    public void SkillNormalRelease(BaseEventData eventData)
    {
        isTriggerSkill = false;
        m_PlayerController.m_PlayerAttributes.StrengthValue = 1;
        m_PlayerController.m_PlayerAttributes.MaxSpeed = 20;
        m_PlayerController.m_PlayerAttributes.ExpansionMultiple = 1;
        AttributeText.text = "力:" + m_PlayerController.m_PlayerAttributes.PowerValue +
    " 体:" + m_PlayerController.m_PlayerAttributes.StrengthValue +
    " 智:" + m_PlayerController.m_PlayerAttributes.IntelligenceValue;
        skillEffect.UpdateAttribute(false);
    }

    public void SkillATrigger(BaseEventData eventData)
    {
        if (slider.value > 0.5f)
        {
            StartCoroutine("SkillCDA");
            slider.value -= 0.5f;
            skillEffect.CmdSkillA(true);
        }
    }

    public void SkillBTrigger(BaseEventData eventData)
    {
        if (slider.value > 0.2f)
        {
            StartCoroutine("SkillCDB");
            slider.value -= 0.2f;
            skillEffect.CmdSkillB();
        }
    }

    public void SkillCTrigger(BaseEventData eventData)
    {
        if (slider.value > 0.4f)
        {
            StartCoroutine("SkillCDC");
            slider.value -= 0.4f;
            skillEffect.CmdSkillC();
        }
    }

    public delegate void skillDeledate(bool isOn);
    skillDeledate skillReleaseDeledate;

    IEnumerator SkillCDA()
    {
        int skillTime = 5;
        int CDTime = 10;
        SkillAButton.GetComponent<EventTrigger>().enabled = false;
        SkillAButton.GetComponent<Image>().color = Color.red;

        yield return new WaitForSeconds(skillTime);
        skillEffect.CmdSkillA(false);
        if (CDTime > skillTime) yield return new WaitForSeconds(CDTime - skillTime);
        SkillAButton.GetComponent<EventTrigger>().enabled = true;
        SkillAButton.GetComponent<Image>().color = Color.white;
    }

    IEnumerator SkillCDB()
    {
        int CDTime = 3;

        SkillBButton.GetComponent<EventTrigger>().enabled = false;
        SkillBButton.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(CDTime);
        SkillBButton.GetComponent<EventTrigger>().enabled = true;
        SkillBButton.GetComponent<Image>().color = Color.white;
    }

    IEnumerator SkillCDC()
    {
        int CDTime = 3;

        SkillCButton.GetComponent<EventTrigger>().enabled = false;
        SkillCButton.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(CDTime);
        SkillCButton.GetComponent<EventTrigger>().enabled = true;
        SkillCButton.GetComponent<Image>().color = Color.white;
    }

    #endregion
    void SliderADD()
    {
        slider.value += m_PlayerController.m_PlayerAttributes.SliderAddVaule * Time.deltaTime;
    }
}
