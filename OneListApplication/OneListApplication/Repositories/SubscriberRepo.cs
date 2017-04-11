﻿using OneListApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Repositories
{
    public class SubscriberRepo
    {
        /* *******************************************************
        * Get all subscriber Group
        * Return: void
        ********************************************************/
        public IEnumerable<SubscriberGroupVM> GetSubscriberGroups()
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<SubscriberGroupVM> subscriberGroups = db.SuscriberGroups.Select(s => new SubscriberGroupVM() { SubscriberGroupID = s.SuscriberGroupID, SubscriberGroupName = s.SuscriberGroupName });
            return subscriberGroups;
        }
        /* *******************************************************
        * Delete subscriber Group
        * Parameter: Int subscriberGroupId
        * Return: out errMsg (string)
        ********************************************************/
        public void DeleteGroup(int subscriberGroupId, out string errMsg)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupToBeUpdated = db.SuscriberGroups.Where(s => s.SuscriberGroupID == subscriberGroupId).FirstOrDefault();
            if (groupToBeUpdated != null)
            {
                db.SuscriberGroups.Remove(groupToBeUpdated);
                db.SaveChanges();
                errMsg = "Group Deleted";
            }
            else
            {
                errMsg = "Group could not be deleted.";
            }
        }
        /* *******************************************************
        * AddUserToGroup
        * Parameter: string userID
        ********************************************************/
        public void AddUserToGroup(SubscriberGroupVM subGroup, string publisherUserId) {
            // TO DO: server side validation & client side validation
            var now = DateTime.UtcNow;
            const string DEFAULT_STATUS = "Active";
            const int DEFAULT_TYPE = 2;
            OneListEntitiesCore db = new OneListEntitiesCore();

            SuscriberGroup sGroup = db.SuscriberGroups.Where(a => a.SuscriberGroupID == subGroup.SubscriberGroupID).FirstOrDefault();
            SuscriberGroupUser newGroupUser = new SuscriberGroupUser();
          //  newGroupUser.SuscriberGroupUserID = subGroup.subscribedUser.UserID;
            newGroupUser.UserID = publisherUserId;
            newGroupUser.SuscriberGroupID = subGroup.SubscriberGroupID;
            newGroupUser.UserTypeID = DEFAULT_TYPE;
            newGroupUser.ListUserStatus = DEFAULT_STATUS;
            newGroupUser.SuscriptionDate = now.ToShortDateString();
            newGroupUser.SuscriberGroup = sGroup; //Add subscriberGroup property !important

            var query = db.SuscriberGroupUsers.Add(newGroupUser);

            db.SaveChanges();
        }

        /* *******************************************************
        * GetGroupDetails
        * Parameter: int GroupID
        * return: SubscriberGroupVM
        ********************************************************/
        public SubscriberGroupVM GetGroupDetails(int id) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupToBeUpdated = db.SuscriberGroups.Where(a => a.SuscriberGroupID == id).FirstOrDefault();
            SubscriberGroupVM sg = new SubscriberGroupVM();
            sg.SubscriberGroupID = groupToBeUpdated.SuscriberGroupID;
            sg.SubscriberGroupName = groupToBeUpdated.SuscriberGroupName;
            sg.UserList = GetAllUsers();
            sg.allSubscribedUsers = GetAllSubscribedUsers(id);
            return sg;
        }
        /* *******************************************************
        * GetAllSubscribedUsers
        * Return: IEnumerable<SelectListItem>
        ********************************************************/
        public IEnumerable<SubscriberGroupUserVM> GetAllSubscribedUsers(int id)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            var subscribedUsers = db.SuscriberGroupUsers
                                    .Where(a=>a.SuscriberGroupID ==id )
                                    .Select(x =>
                                            new SubscriberGroupUserVM
                                            {
                                                UserID = x.UserID,
                                                ListUserStatus = x.ListUserStatus,
                                                UserTypeID = x.UserTypeID,
                                            });
            return subscribedUsers;
        }

        /* *******************************************************
        * GetAllUsers
        * Return: IEnumerable<SelectListItem>
        ********************************************************/
        public IEnumerable<SelectListItem> GetAllUsers() {
            OneListEntitiesCore db = new OneListEntitiesCore();
            var categories = db.Users
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.UserID.ToString(),
                                    Text = x.UserName
                                });

            return new SelectList(categories, "Value", "Text");
        }
        /* *******************************************************
        * UpdateGroup
        * Return: bool
        ********************************************************/
        public bool UpdateGroup(SubscriberGroupVM subscriberGroup)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroup groupUpdated = db.SuscriberGroups.Where(a => a.SuscriberGroupID == subscriberGroup.SubscriberGroupID).FirstOrDefault();
            groupUpdated.SuscriberGroupName = subscriberGroup.SubscriberGroupName;

            db.SaveChanges();
            return true;
        }
        /* *******************************************************
        * GetSubscriberGroupUsers
        * Return: IEnumerable<SubscriberGroupUserVM>
        ********************************************************/
        public IEnumerable<SubscriberGroupUserVM> GetSubscriberGroupUsers(int id)
        {
            OneListEntitiesCore db = new OneListEntitiesCore();
            IEnumerable<SubscriberGroupUserVM> subscriberGroupUsers = db.SuscriberGroupUsers.Select(a => new SubscriberGroupUserVM() { SubscriberGroupID = id, UserID = a.UserID, ListUserStatus = a.ListUserStatus, UserTypeID = a.UserTypeID, SubscriptionDate = a.SuscriptionDate });
            return subscriberGroupUsers;
        }
        /* *******************************************************
        * DeleteSubscriber
        * Return: void
        ********************************************************/
        public void DeleteSubscriber(string userId, int id, out string errMsg) {
            OneListEntitiesCore db = new OneListEntitiesCore();
            SuscriberGroupUser groupToBeUpdated =  db.SuscriberGroupUsers
                                                    .Where(s => s.SuscriberGroupUserID == id
                                                        && s.SuscriberGroupID == id
                                                    ).FirstOrDefault();

            if (groupToBeUpdated != null)
            {
                db.SuscriberGroupUsers.Remove(groupToBeUpdated);
                db.SaveChanges();
                errMsg = "Subscriber Deleted";
            }
            else
            {
                errMsg = "Subscriber could not be deleted.";
            }
        }
    }
}