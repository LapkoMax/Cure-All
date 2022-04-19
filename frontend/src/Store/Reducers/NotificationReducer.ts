import { NotificationData } from "../../Api/NotificationsData";
import {
  gettingNotificationAction,
  gettingNotificationsAction,
  gotNotificationAction,
  gotNotificationsAction,
} from "../ActionCreators/NotificationActionCreators";
import {
  GETTINGNOTIFICATION,
  GETTINGNOTIFICATIONS,
  GOTNOTIFICATION,
  GOTNOTIFICATIONS,
} from "../Actions/NotificationActions";

type NotificationsActions =
  | ReturnType<typeof gettingNotificationsAction>
  | ReturnType<typeof gotNotificationsAction>
  | ReturnType<typeof gettingNotificationAction>
  | ReturnType<typeof gotNotificationAction>;

export interface NotificationsState {
  readonly notifications: NotificationData[];
  readonly notification?: NotificationData | null;
  readonly loading: boolean;
}

const initialState: NotificationsState = {
  notifications: [],
  notification: null,
  loading: false,
};

export const notificationReducer = (
  state = initialState,
  action: NotificationsActions,
) => {
  switch (action.type) {
    case GETTINGNOTIFICATIONS: {
      return {
        ...state,
        loading: true,
        notifications: [],
      };
    }
    case GOTNOTIFICATIONS: {
      return {
        ...state,
        loading: false,
        notifications: action.notifications,
      };
    }
    case GETTINGNOTIFICATION: {
      return {
        ...state,
        loading: true,
        notification: null,
      };
    }
    case GOTNOTIFICATION: {
      return {
        ...state,
        loading: false,
        notification: action.notification,
      };
    }
  }
  return state;
};
