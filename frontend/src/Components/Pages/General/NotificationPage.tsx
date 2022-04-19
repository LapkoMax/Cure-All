/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { getAppointment } from "../../../Api/AppointmentsData";
import {
  confirmNotification,
  getNotification,
  rejectNotification,
} from "../../../Api/NotificationsData";
import {
  gettingAppointmentAction,
  gotAppointmentAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingNotificationAction,
  gotNotificationAction,
} from "../../../Store/ActionCreators/NotificationActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
} from "../../../Styles/Common/Buttons";
import {
  notificationAppointment,
  notificationContainer,
  notificationDesctiption,
} from "../../../Styles/Notifications/NotificationPageStyles";
import { notificationTitle } from "../../../Styles/Notifications/NotificationStyles";
import { Appointment } from "../../Appointments/Appointment";
import { Page } from "../../General/Page";

export const NotificationPage = () => {
  const { notificationId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const userToken = useSelector((state: AppState) => state.identity.token);
  const notification = useSelector(
    (state: AppState) => state.notifiactions.notification,
  );
  const notificationLoading = useSelector(
    (state: AppState) => state.notifiactions.loading,
  );
  const appointment = useSelector(
    (state: AppState) => state.appointments.appointment,
  );
  const appointmentLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );

  useEffect(() => {
    const doGetNotification = async (notificationId?: string) => {
      dispatch(gettingNotificationAction());
      let result = await getNotification(notificationId, userToken);
      if (result.status === 401) dispatch(signOutUserAction(location.pathname));
      else if (result.status === 200)
        dispatch(gotNotificationAction(result.data));
    };
    doGetNotification(notificationId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    const doGetAppointment = async (appointmentId?: string) => {
      dispatch(gettingAppointmentAction());
      let result = await getAppointment(appointmentId, userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      else if (result.responseStatus === 200)
        dispatch(gotAppointmentAction(result.data));
    };
    if (notification !== undefined && notification !== null)
      doGetAppointment(notification.appointmentId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [notification]);

  const onAppointmentConfirm = async () => {
    var result = await confirmNotification(notificationId, userToken);
    if (result) navigate("/notifications");
  };

  const onAppointmentReject = async () => {
    var result = await rejectNotification(notificationId, userToken);
    if (result) navigate("/notifications");
  };

  return (
    <Page>
      {notificationLoading ? (
        <div>Загрузка...</div>
      ) : (
        <div css={notificationContainer}>
          <div css={notificationDesctiption}>
            {notification?.message.split(". ")[0]}
          </div>
          {appointmentLoading ? (
            <div>Загрузка...</div>
          ) : (
            <div>
              <div css={notificationTitle}>Посещение:</div>
              <div css={notificationAppointment}>
                <Appointment appointment={appointment} />
              </div>
              {notification?.message.split(". ")[0] ===
                "Вам поступил новый запрос на посещение" && (
                <FormButtonContainer className="d-flex justify-content-around">
                  <PrimaryButton onClick={onAppointmentConfirm}>
                    Принять посещение
                  </PrimaryButton>
                  <DangerButton onClick={onAppointmentReject}>
                    Отклонить посещение
                  </DangerButton>
                </FormButtonContainer>
              )}
            </div>
          )}
        </div>
      )}
    </Page>
  );
};
