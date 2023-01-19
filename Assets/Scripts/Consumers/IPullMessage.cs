using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consumer;

public interface IPullMessage
{
    public void addToList(Consumer.Message newMessage);
    public void updatePushMessages(Consumer.Message newMessage);
    public void clearPullList();
    public void clearPushList();
    public List<Consumer.Message> getPushList();
    public void updateMessages();
}
