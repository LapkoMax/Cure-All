/** @jsxImportSource @emotion/react */
import { NotificationData } from "../../Api/NotificationsData";
import {
  notificationComponent,
  notificationList,
} from "../../Styles/Notifications/NotificationListStyles";
import { Notification } from "./Notification";

type Props = {
  data: NotificationData[];
};

export const NotificationList = ({ data }: Props) => {
  return (
    <ul css={notificationList}>
      {data.map((notif) => (
        <li css={notificationComponent}>
          <Notification data={notif} />
        </li>
      ))}
    </ul>
  );
};
