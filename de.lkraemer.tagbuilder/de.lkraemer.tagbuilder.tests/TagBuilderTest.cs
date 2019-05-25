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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace de.lkraemer.tagbuilder.tests
{
    [TestClass]
    public class TagBuilderTest
    {
        /// <summary>
        /// tests the generation of the attributes
        /// </summary>
        [TestMethod]
        public void TestGetRenderedAttributes()
        {
            string elementTagName = "div";
            TagBuilder tagBuilder = new TagBuilder(elementTagName);

            string idValue = "AnSimpleId";
            tagBuilder.AddAttribute("id", idValue);
            string classValue = "panel text-center";
            tagBuilder.AddAttribute("class", classValue);

            string html = tagBuilder.GetRenderedAttributes();

            Assert.IsFalse(html.Contains("<" + elementTagName)); // the element must not contains the start tag
            Assert.IsFalse(html.Contains("</" + elementTagName + ">")); // the element not must contains the end tag
            Assert.IsTrue(html.Contains("id=\"" + idValue + "\"")); // the element must contain an id
            Assert.IsTrue(html.Contains("class=\"" + classValue + "\"")); // the element must contain a class
        }

        /// <summary>
        /// tests the generation of the html element
        /// </summary>
        [TestMethod]
        public void TestRenderAsString()
        {
            string elementTagName = "p";
            TagBuilder tagBuilder = new TagBuilder(elementTagName);

            string classValue = "is--text text-center";
            tagBuilder.AddAttribute("class", classValue);

            string html = tagBuilder.RenderAsString();

            Assert.IsTrue(html.Contains("<" + elementTagName)); // the element must contains the start tag
            Assert.IsTrue(html.Contains("</" + elementTagName + ">")); // the element must contains the end tag
            Assert.IsFalse(html.Contains("id=\"")); // the element must not contain an id
            Assert.IsTrue(html.Contains("class=\"" + classValue + "\"")); // the element must contain a class
        }

        /// <summary>
        /// tests the generation of an input html element
        /// </summary>
        [TestMethod]
        public void TestRenderInputAsString()
        {
            string elementTagName = "input";
            TagBuilder tagBuilder = new TagBuilder(elementTagName);

            string classValue = "is--text text-center";
            tagBuilder.AddAttribute("class", classValue);

            string html = tagBuilder.RenderAsString();

            Assert.IsTrue(html.Contains("<" + elementTagName)); // the element must contains the start tag
            Assert.IsTrue(html.Contains(" />")); // the element must contains the start tag
            Assert.IsFalse(html.Contains("</" + elementTagName + ">")); // the element must not contains the e´xplicit end tag
            Assert.IsFalse(html.Contains("id=\"")); // the element must not contain an id
            Assert.IsTrue(html.Contains("class=\"" + classValue + "\"")); // the element must contain a class
        }
    }
}
