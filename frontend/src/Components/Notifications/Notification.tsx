/** @jsxImportSource @emotion/react */
import { Link } from "react-router-dom";
import { NotificationData } from "../../Api/NotificationsData";
import {
  notificationContainer,
  unreadNotificationSpan,
} from "../../Styles/Notifications/NotificationStyles";

type Props = {
  data: NotificationData;
};

export const Notification = ({ data }: Props) => {
  return (
    <Link to={data.id} css={notificationContainer}>
      {!data.readed && <span css={unreadNotificationSpan}>Новое</span>}
      {data.message.split(". ")[0]}
    </Link>
  );
};
