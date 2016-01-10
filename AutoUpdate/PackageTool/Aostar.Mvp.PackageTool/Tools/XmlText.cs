/////////////////////////////////////////////////////////////////////////////
//
// 文 件 名: XmlObject.cs
//
// 功能介绍: 
//
// 创 建 者: 郭正奎
// 创建时间: 2008-12-22 17:19
// 修订历史: 2008-12-22 17:19
//
// 益体康(北京)科技有限公司 (c)2007-2008 保留所有版权
//
// contact@etcomm.cn
// http://www.etcomm.cn
//
/////////////////////////////////////////////////////////////////////////////

using System.Xml;
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
/// <summary>
/// XmlObject
/// </summary>
public class XmlText : XmlDocument
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    public XmlText()
    {

    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filename">生成的Xml文件名</param>
    public XmlText(string filename)
        : this()
    {
        FileName = filename;
        FileFullName = FileName;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename">生成的Xml文件名</param>
    /// <param name="filepath">生成的Xml文件路径</param>
    public XmlText(string filename, string filepath)
        : this(filename)
    {
        _FilePath = filepath;
        if (!string.IsNullOrEmpty(FilePath))
        {
            FileFullName = FilePath + @"\" + FileName;
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }
    }

    #endregion

    #region 私有字段

    private string _FileName = "";
    private string FileName
    {
        get
        {
            return _FileName;
        }
        set
        {
            _FileName = value;
        }
    }

    private string _FilePath = "";
    private string FilePath
    {
        get
        {
            return _FilePath;
        }
        set
        {
            _FilePath = value;
        }
    }

    private string FileFullName = "";

    private XmlNode MainElement = null;

    private readonly List<string> SectionList = new List<string>();
    #endregion

    #region 公有方法

    /// <summary>
    /// 选择匹配 XPath 表达式的第一个 XmlNode。
    /// 异常:
    ///   System.Xml.XPath.XPathException:
    ///     XPath 表达式包含前缀。
    /// </summary>
    /// <param name="xPath"> XPath 表达式。</param>
    /// <returns> 与 XPath 查询匹配的第一个 XmlNode；如果未找到任何匹配节点，则为 null。</returns>
    public XmlNode FindNode(string xPath)
    {
        XmlNode xmlNode = this.SelectSingleNode(xPath);
        Debug.Assert(xmlNode != null, xPath + " node did not find!");

        return xmlNode;
    }
    /// <summary>
    /// 选择匹配 XPath 表达式的第一个 XmlNode 的值。
    /// </summary>
    /// <param name="xPath">XPath 表达式。</param>
    /// <returns>返回节点值。</returns>
    public string GetNodeValue(string xPath)
    {
        XmlNode xmlNode = this.SelectSingleNode(xPath);
        Debug.Assert(xmlNode != null, xPath + " node did not find!");

        return xmlNode.InnerText;
    }
    /// <summary>
    /// 获取xPath节点的所有子节点。
    /// </summary>
    /// <param name="xPath">XPath 表达式。</param>
    /// <returns>一个 System.Xml.XmlNodeList，它包含节点的所有子节点。如果没有子节点，该属性返回空 System.Xml.XmlNodeList。</returns>
    public XmlNodeList GetNodeList(string xPath)
    {
        XmlNodeList nodeList = this.SelectSingleNode(xPath).ChildNodes;
        Debug.Assert(nodeList != null, xPath + " node did not find!");
        return nodeList;
    }
    /// <summary>
    /// 获取一个 System.Xml.XmlAttributeCollection，它包含xPath节点的属性。
    /// </summary>
    /// <param name="xPath">XPath 表达式。</param>
    /// <returns>一个 XmlAttributeCollection，它包含xPath节点的属性。如果节点为 XmlNodeType.Element 类型，则返回该节点的属性。否则，该属性将返回null</returns>
    public XmlAttributeCollection GetAttributes(string xPath)
    {
        XmlAttributeCollection attributes = this.SelectSingleNode(xPath).Attributes;
        Debug.Assert(attributes != null, xPath + " node did not find!");

        return attributes;
    }

    private bool IsSectionAvailable(string Section)
    {
        return SectionList.Contains(Section);
    }


    ///<summary>
    ///</summary>
    ///<param name="Element"></param>
    ///<param name="Key"></param>
    ///<returns></returns>
    ///<exception cref="Exception"></exception>
    public string GetAttributeValue(XmlNode Element, string Key)
    {
        string Value = "";
        try
        {
            Value = FindNode(Key).Attributes[Name].Value;
        }
        catch
        {
            throw new Exception(Name);
        }
        return Value;
    }

    ///<summary>
    ///</summary>
    ///<param name="Section"></param>
    public void AddSection(string Section)
    {
        AddSection(MainElement, Section);
    }
    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Section"></param>
    public void AddSection(string Parentelement, string Section)
    {
        AddSection(FindNode(Parentelement), Section);
    }
    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Section"></param>
    public void AddSection(XmlNode Parentelement, string Section)
    {
        if (IsSectionAvailable(Section)) return;

        XmlNode newElement = this.CreateElement(Section);
        XmlNode CommentElement = this.CreateNode(XmlNodeType.Comment, "NodeComment", null);
        CommentElement.InnerText = "Item List!";
        newElement.AppendChild(CommentElement);
        Parentelement.AppendChild(newElement);
        SectionList.Add(Section);

    }

    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="element"></param>
    ///<param name="value"></param>
    public void AddElement(string Parentelement, string element)
    {
        XmlNode ItemElement = this.CreateNode(XmlNodeType.Element, element, "");
        ItemElement.InnerText = "";
        FindNode(Parentelement).AppendChild(ItemElement);
    }

    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="element"></param>
    ///<param name="value"></param>
    public void AddElement(string Parentelement, string element,string value)
    {
        XmlNode ItemElement = this.CreateNode(XmlNodeType.Element, element, "");
        ItemElement.InnerText = value;
        FindNode(Parentelement).AppendChild(ItemElement);
    }

    ///<summary>
    ///</summary>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void AddItem(string Key, string value)
    {
        AddItem(MainElement, Key, value);
    }
    ///<summary>
    ///</summary>
    ///<param name="Element"></param>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void AddItem(string Element, string Key, string value)
    {
        AddItem(FindNode(Element), Key, value);
    }
    ///<summary>
    ///</summary>
    ///<param name="Element"></param>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void AddItem(XmlNode Element, string Key, string value)
    {
        XmlNode ItemElement = this.CreateNode(XmlNodeType.Element, "Item", "");
        ItemElement.Attributes.Append(AddAttribute("key", Key));
        ItemElement.Attributes.Append(AddAttribute("value", value));
        Element.AppendChild(ItemElement);
    }

    ///<summary>
    ///</summary>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    ///<returns></returns>
    public XmlAttribute AddAttribute(string Key, string value)
    {
        XmlAttribute newKey = this.CreateAttribute(Key);
        newKey.InnerText = value;
        return newKey;
    }

    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void SetAttributeKey(string Parentelement, string Key, string value)
    {
        //SetAttributeValue(GetItem(Parentelement),Key, value);

    }
    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void SetateAttributeValue(string Parentelement, string Key, string value)
    {
        //SetAttributeKey(GetItem(Parentelement),Key, value);
    }

    ///<summary>
    ///</summary>
    ///<param name="Sectionelement"></param>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void SetAttributeKey(XmlNode Sectionelement, string Key, string value)
    {
        XmlNode Temp = null;
        Temp = GetItem(Sectionelement, Key);
        Temp.Attributes["key"].Value = value;
    }
    ///<summary>
    ///</summary>
    ///<param name="Sectionelement"></param>
    ///<param name="Key"></param>
    ///<param name="value"></param>
    public void SetAttributeValue(XmlNode Sectionelement, string Key, string value)
    {
        XmlNode Temp = null;
        Temp = GetItem(Sectionelement, Key);
        Temp.Attributes["value"].Value = value;
    }
    /// <summary>
    /// 获取存在指定属性Key的XmlNode
    /// </summary>
    /// <param name="Sectionelement">XmlNode的上一级XmlNode</param>
    /// <param name="Key">要查找的属性Key</param>
    /// <returns>返回XmlNode</returns>
    public XmlNode GetItem(XmlNode Sectionelement, string Key)
    {
        XmlNode Temp = null;
        //foreach (XmlNode item in Sectionelement.ChildNodes)
        //{
        //    if (item.Attributes["key"] == Key)
        //    {
        //        Temp = item;
        //        break;
        //    }
        //}
        return Temp;
    }


    ///<summary>
    ///</summary>
    ///<param name="Element"></param>
    public void RemoveElement(string Element)
    {
        RemoveElement(MainElement, Element);
    }
    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Element"></param>
    public void RemoveElement(XmlNode Parentelement, string Element)
    {
        RemoveElement(Parentelement, FindNode(Element));
    }
    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Element"></param>
    public void RemoveElement(XmlNode Parentelement, XmlNode Element)
    {
        SectionList.Remove(Element.Name);
        Element.RemoveAll();
        Parentelement.RemoveChild(Element);

    }

    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Attribute"></param>
    public void RemoveAttributes(string Parentelement, string Attribute)
    {
        // FindNode(Parentelement).Attributes.Remove(Attribute);
    }
    ///<summary>
    ///</summary>
    ///<param name="Parentelement"></param>
    ///<param name="Attribute"></param>
    public void RemoveAttributes(XmlNode Parentelement, XmlAttribute Attribute)
    {
        Parentelement.Attributes.Remove(Attribute);
    }


    ///<summary>
    ///</summary>
    ///<param name="element"></param>
    public void LoadSection(XmlNode element)
    {
        foreach (XmlNode item in element.ChildNodes)
        {
            if (item.Attributes.Count > 0)
            {
                if (XmlConvert.ToBoolean(item.Attributes["IsSection"].InnerText))
                {
                    SectionList.Add(item.InnerText);
                }
            }
        }
    }

    /// <summary>
    /// 保存XML文件
    /// </summary>
    public void Save()
    {
        try
        {
            Save(FileFullName);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    ///<summary>
    ///</summary>
    ///<returns></returns>
    ///<exception cref="Exception"></exception>
    public bool Load()
    {
        bool IsOk = false;
        try
        {
            if (File.Exists(FileFullName))
            {
                Load(FileFullName);
            }
           

            IsOk = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return IsOk;
    }

    #endregion

    private void CreateXml(string RootElement)
    {
        this.AppendChild(this.CreateXmlDeclaration("1.0", "UTF-8", null));
        MainElement = this.CreateElement(RootElement);
        this.AppendChild(MainElement);
        XmlNode CommentElement = this.CreateNode(XmlNodeType.Comment, "MainComment", null);
        CommentElement.InnerText = "Node List!";
        MainElement.AppendChild(CommentElement);
        this.Save();
    }
}