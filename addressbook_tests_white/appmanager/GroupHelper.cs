using System;
using System.Collections.Generic;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;


namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string GROUPDELETIONWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach(TreeNode item in root.Nodes)
            {
                list.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
              
            CloseGroupsDialogue(dialogue);
            return list;
        }

        public void AddGroup(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textbox = (TextBox)dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textbox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialogue(dialogue);
        }

        private void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        private Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

       /* private Window OpenGroupsDeletionDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }*/

        public int GetGroupCount()
        {
            int count = 0;
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                count++;
            }
            CloseGroupsDialogue(dialogue);
            return count;
        }

        public void Remove(GroupData toBeRemoved)
        {
            Window dialogue = OpenGroupsDialogue();

            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            var toclick = root.GetElement(SearchCriteria.ByText(toBeRemoved.Name));
            toclick.SetFocus();
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
           // Window window = dialogue.Get<Window>("DeleteGroupForm").;
            Window window = dialogue.ModalWindow(GROUPDELETIONWINTITLE);
            window.Get<RadioButton>("uxDeleteAllRadioButton").Click();
            window.Get<Button>("uxOKAddressButton").Click();

            CloseGroupsDialogue(dialogue);
        }

        public GroupHelper IsEmptyCheck()
        {
            int count = GetGroupCount();

            if (count == 0)
            {
                GroupData fortest = new GroupData();
                fortest.Name = "test";
                AddGroup(fortest);
            }
            return this;
        }
    }
}