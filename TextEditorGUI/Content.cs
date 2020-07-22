using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorGUI
{
    public class Content
    {
        public string text = "";
        protected Stack<string> undoStack;
        protected Stack<string> redoStack;
        private bool isFirst = true;
        private bool isUndo = false;
        public void textChanged(string _text)
        {
            if (!isFirst)
            {
                isFirst = true;
            }
            else if(!isUndo)
            {
                this.undoStack.Push(_text);
                isUndo = false;
            }
            else
            {
                isUndo = false;
            }
        }
        //Doesn't get rid of last thing
        public string undo()
        {

            if (undoStack.Count > 1)
            {
                isUndo = true;
                var popped = this.undoStack.Pop();
                this.redoStack.Push(popped);
                this.text = popped;
            }
            return this.text;
        }
        //If i redo, it redoes 2 times and then everything is broken
        public string redo()
        {
            if (!(redoStack.Count <= 1))
            {
                var popped = this.redoStack.Pop();
                this.undoStack.Push(popped);
                this.text = popped;
            }
            return this.text;
        }
        public void save(string _path)
        {
            var file = new FileInfo(_path);
            File.WriteAllText(file.FullName, text);
        }
        //For new
        public Content()
        {
            this.undoStack = new Stack<string>();
            this.redoStack = new Stack<string>();
        }
        //For loading
        public Content(string _text, Stack<string> _undoStack, Stack<string> _redoStack)
        {
            this.text = _text;
            this.undoStack = _undoStack;
            this.redoStack = _redoStack;
        }
    }
}
