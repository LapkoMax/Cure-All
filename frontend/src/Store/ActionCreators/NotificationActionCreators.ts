import { NotificationData } from "../../Api/NotificationsData";
import {
  GETTINGNOTIFICATION,
  GETTINGNOTIFICATIONS,
  GOTNOTIFICATION,
  GOTNOTIFICATIONS,
} from "../Actions/NotificationActions";

export const gettingNotificationsAction = () =>
  ({
    type: GETTINGNOTIFICATIONS,
  } as const);

export const gotNotificationsAction = (notifications: NotificationData[]) =>
  ({
    type: GOTNOTIFICATIONS,
    notifications: notifications,
  } as const);

export const gettingNotificationAction = () =>
  ({
    type: GETTINGNOTIFICATION,
  } as const);

export const gotNotificationAction = (notification: NotificationData | null) =>
  ({
    type: GOTNOTIFICATION,
    notification: notification,
  } as const);
