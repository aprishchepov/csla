﻿using System;
using System.Collections.Generic;

namespace Csla.Rules
{
  /// <summary>
  /// Parses a rule:// URI to provide
  /// easy access to the parts of the URI.
  /// </summary>
  public class RuleUri
  {
    private Uri _uri;

    /// <summary>
    /// Creates an instance of the object
    /// by parsing the provided rule:// URI.
    /// </summary>
    /// <param name="ruleString">The rule:// URI.</param>
    public RuleUri(string ruleString)
      : this(new Uri(ruleString))
    { }

    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    /// <param name="uri">The rule:// URI.</param>
    public RuleUri(Uri uri)
    {
      _uri = uri;
      if (_uri.Scheme != "rule")
        throw new ArgumentException("RuleUri.Scheme");
    }

    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    /// <param name="rule">Rule object.</param>
    /// <param name="property">Property to which rule applies.</param>
    public RuleUri(IBusinessRule rule, Csla.Core.IPropertyInfo property)
      : this("rule://" + rule.GetType().FullName + "/" + ((property == null) ? "null" : property.Name))
    { }

    /// <summary>
    /// Parses a rule:// URI.
    /// </summary>
    /// <param name="ruleString">
    /// Text representation of a rule:// URI.</param>
    /// <returns>A populated RuleDescription object.</returns>
    public static RuleUri Parse(string ruleString)
    {
      return new RuleUri(ruleString);
    }

    public override string ToString()
    {
      return _uri.ToString();
    }

    public void AddQueryParameter(string key, string value)
    {
      var uriText = ToString();
      if (uriText.Contains("?"))
        uriText = uriText + "&" + Uri.EscapeUriString(key) + "=" + Uri.EscapeUriString(value);
      else
        uriText = uriText + "?" + Uri.EscapeUriString(key) + "=" + Uri.EscapeUriString(value);
      _uri = new Uri(uriText);
    }

    /// <summary>
    /// Gets the name of the type containing
    /// the rule method.
    /// </summary>
    public string RuleTypeName
    {
      get { return Uri.UnescapeDataString(_uri.Host); }
    }

    /// <summary>
    /// Gets the name of the property with which
    /// the rule is associated.
    /// </summary>
    public string PropertyName
    {
      get { return _uri.LocalPath.Split('/')[1]; }
    }
  }
}