using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendSystem : MonoBehaviour
{
    public Transform content;//ScrollView下Content
    public Dictionary<string, FriendGroup> friendsDic= new Dictionary<string, FriendGroup>();//所有好友列表
    private void Awake()
    {
        //在这里简单数据代替了一下，进行初始化好友列表
        friendsDic.Add("家人",new FriendGroup("家人"));

        friendsDic["家人"].friends.Add(new Friend("父亲"));
        friendsDic["家人"].friends.Add(new Friend("母亲"));
        
        friendsDic["家人"].Init(content);

        friendsDic.Add("好友", new FriendGroup("好友"));

        friendsDic["好友"].friends.Add(new Friend("1111"));
        friendsDic["好友"].friends.Add(new Friend("2222"));

        friendsDic["好友"].Init(content);

        friendsDic.Add("老师", new FriendGroup("老师"));

        friendsDic["老师"].friends.Add(new Friend("3333"));
        friendsDic["老师"].friends.Add(new Friend("4444"));

        friendsDic["老师"].Init(content);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/// <summary>
/// 好友组
/// </summary>
public class FriendGroup
{
    public string groupName;
    public List<Friend> friends=new List<Friend>();

    public GameObject gameObject;
    public Text label;
    public Toggle toggle;
    public FriendGroup(string groupName)
    {
        this.groupName = groupName;
    }
    /// <summary>
    /// 初始化好友组
    /// </summary>
    /// <param name="parent"></param>
    public void Init(Transform parent)
    {
        gameObject = GameObject.Instantiate(Resources.Load<GameObject>("FriendType"), parent);
        label =gameObject.GetComponentInChildren<Text>();
        toggle =gameObject.GetComponent<Toggle>();
        //初始化好友组内成员
        for (int i = 0; i < friends.Count; i++)
        {
            friends[i].Init(parent);
            friends[i].gameObject.SetActive(false);
        }
        if (label!=null)
        {
            label.text = groupName;
        }
        if (toggle!=null)
        {
            toggle.isOn = false;//初始化
            toggle.onValueChanged.AddListener((flag) =>
            {
                //打开或关闭
                foreach (var item in friends)
                {
                    item.gameObject.SetActive(flag);
                }
            });

        }
        
    }
}
/// <summary>
/// 好友
/// </summary>
public class Friend
{
    public string friendName;

    public GameObject gameObject;
    public Text label;
    public Toggle toggle;
    public Friend(string friendName)
    {
        this.friendName = friendName;
    }
    /// <summary>
    /// 初始化好友
    /// </summary>
    /// <param name="parent"></param>
    public void Init(Transform parent)
    {
        gameObject = GameObject.Instantiate(Resources.Load<GameObject>("FriendItem"), parent);
        label = gameObject.GetComponentInChildren<Text>();
        toggle = gameObject.GetComponent<Toggle>();
        if (label != null)
        {
            label.text = friendName;
        }
        if (toggle != null)
        {
            toggle.isOn = false;//初始化
            toggle.onValueChanged.AddListener((flag) =>
            {
                //可以修改逻辑，比如打开聊天界面
                Debug.Log("打开好友界面:"+friendName+":"+flag);
            });
        }
    }
}