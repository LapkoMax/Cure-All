import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { getUserNotifications } from "../../../Api/NotificationsData";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingNotificationsAction,
  gotNotificationsAction,
} from "../../../Store/ActionCreators/NotificationActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import { Page } from "../../General/Page";
import { NotificationList } from "../../Notifications/NotificationList";

export const NotificationsPage = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const notifications = useSelector(
    (state: AppState) => state.notifiactions.notifications,
  );
  const notificationsLoading = useSelector(
    (state: AppState) => state.notifiactions.loading,
  );

  useEffect(() => {
    const doGetUserNotifications = async (userId?: string) => {
      dispatch(gettingNotificationsAction());
      var results = await getUserNotifications(userId, userToken);
      if (results.status === 401)
        dispatch(signOutUserAction(location.pathname));
      else if (results.status === 200)
        dispatch(gotNotificationsAction(results.data));
    };
    doGetUserNotifications(user?.id);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user]);

  return (
    <Page>
      {notificationsLoading ? (
        <div>Загрузка...</div>
      ) : (
        <NotificationList data={notifications} />
      )}
    </Page>
  );
};
