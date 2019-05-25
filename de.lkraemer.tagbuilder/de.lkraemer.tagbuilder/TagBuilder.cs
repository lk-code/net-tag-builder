/**
 * MIT License
 * 
 * Copyright (c) 2019 lk-code
 * see more at https://github.com/lk-code/net-tag-builder
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Collections.Generic;
using System.Text;

namespace de.lkraemer.tagbuilder
{
    /// <summary>
    /// the TagBuilder class
    /// see more at https://github.com/lk-code/net-tag-builder
    /// </summary>
    public class TagBuilder
    {
        #region # public accessable properties #

        /// <summary>
        /// a list of the attributes for this tag
        /// </summary>
        public Dictionary<string, string> Attributes { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// the current content for this tag
        /// </summary>
        public string InnerContent { get; private set; } = string.Empty;

        #endregion

        #region # private properties #

        /// <summary>
        /// the name of the tag (div, a, h1, etc.)
        /// </summary>
        private string tagName { get; set; } = string.Empty;

        /// <summary>
        /// contains a list of tags without a explicit tag end (input, br, etc.)
        /// </summary>
        private List<string> singleTagNames { get; set; } = new List<string>() {
            "area", "base", "basefont",
            "br", "col", "embed",
            "frame", "hr", "img",
            "input", "link", "mark",
            "param", "source", "track",
            "circle", "rect", "polygon",
            "ellipse"
        };

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        public TagBuilder(string tagName)
        {
            this.tagName = tagName;
        }

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddAttribute(string name, string value)
        {
            this.Attributes.Add(name, value);
        }

        /// <summary>
        /// adds an innerhtml content to the tag content
        /// </summary>
        /// <param name="content">the content as string</param>
        public void AddInnerContent(string content)
        {
            this.InnerContent += content;
        }

        /// <summary>
        /// renders the whole tag to an string
        /// </summary>
        /// <returns></returns>
        public string RenderAsString()
        {
            string html = "";

            html += "<" + this.tagName + "";
            html += this.GetRenderedAttributes();

            if(this.singleTagNames.Contains(this.tagName.ToLower())
                && (string.IsNullOrEmpty(this.InnerContent) || string.IsNullOrWhiteSpace(this.InnerContent)))
            {
                html += " />";
            } else
            {
                html += ">";
                html += this.InnerContent;
                html += "</" + this.tagName + ">";
            }

            return html;
        }

        /// <summary>
        /// returns all attributes as a rendered string
        /// </summary>
        /// <returns></returns>
        public string GetRenderedAttributes()
        {
            StringBuilder attributesBuilder = new StringBuilder();

            foreach (var attributePair in this.Attributes)
            {
                if (!string.IsNullOrEmpty(attributePair.Value) && !string.IsNullOrWhiteSpace(attributePair.Value))
                {
                    string attributeResultScheme = " {0}=\"{1}\"";
                    string attributeResult = string.Format(attributeResultScheme, attributePair.Key, attributePair.Value);

                    attributesBuilder.Append(attributeResult);
                }
            }

            string attributes = attributesBuilder.ToString();

            return attributes;
        }

        #endregion
    }
}
