using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHpBar : MonoBehaviour
{
    [SerializeField]
    private GameObject HpBar;       //HP�� ������
    private Vector2 createPoint = new Vector2(300,50);  //���� ��ġ
    private int MaxHP = 100;        //�ִ� HP
    private int MinHP = 0;          //�ּ� HP
    private float CurrentHp = 100.0f;    //���� HP
    TextMeshProUGUI text = null;   //ü�� ����
    void Start()
    {
        if (HpBar == null)
            return;
        HpBar = Instantiate(HpBar, createPoint, Quaternion.identity, GameObject.Find("Canvas").transform);        //HP�� ������ ����
        HpBar.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 1.0f;
        text = HpBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text.text = "100 / 100"; //ü�� 100���� ����
        if (text == null)
            return;
    }

 
    public void Damage(int damage)
    {
        CurrentHp -= damage;        //���� ü�¿��� ������ ������ŭ ����

        HpBar.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = CurrentHp/100;      //ü�� ������ ����
        text.text = CurrentHp.ToString() + " / 100";
        if (CurrentHp <= MinHP)
            return;         //���߿� �������� �ڵ� �߰�

    }
}
