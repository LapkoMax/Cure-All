/** @jsxImportSource @emotion/react */
import { useEffect, useState } from "react";
import { getDoctorAmount } from "../../../Api/DoctorsData";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  homePageContainer,
  homePageGeneralInformation,
  homePageTitle,
  homePageTodayInf,
} from "../../../Styles/HomePageStyles";
import {
  getPatient,
  getPatientAmount,
  PatientData,
} from "../../../Api/PatientsData";
import {
  getAppointmentAmount,
  getCompletedAppointmentAmount,
  getTodayAppointmentsForDoctor,
  getTodayAppointmentsForPatientCard,
} from "../../../Api/AppointmentsData";
import { AppointmentList } from "../../Appointments/AppointmentList";
import {
  gettingAppointmentsAction,
  gotAppointmentsAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { useLocation } from "react-router-dom";

export const HomePage = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const todayAppointments = useSelector(
    (state: AppState) => state.appointments.appointments,
  );
  const appointmentsLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const [doctorAmount, setDoctorAmount] = useState(0);
  const [patientAmount, setPatientAmount] = useState(0);
  const [appointmentAmount, setAppointmentAmount] = useState(0);
  const [completedAppointmentAmount, setCompletedAppointmentAmount] =
    useState(0);
  const [loginedPatient, setLoginedPatient] = useState<PatientData | null>(
    null,
  );

  useEffect(() => {
    const doGetPatient = async () => {
      var result = await getPatient(userToken, user?.id);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      setLoginedPatient(result.data);
    };
    if (user?.type === "Patient") doGetPatient();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user]);

  useEffect(() => {
    const doGetAppointments = async () => {
      dispatch(gettingAppointmentsAction());
      if (user?.type === "Doctor") {
        var results = await getTodayAppointmentsForDoctor(user.id, userToken);
        if (results.responseStatus === 401)
          dispatch(signOutUserAction(location.pathname));
        else if (results.responseStatus === 200)
          dispatch(gotAppointmentsAction(results.data));
      } else if (user?.type === "Patient") {
        results = await getTodayAppointmentsForPatientCard(
          loginedPatient?.patientCardId,
          userToken,
        );
        if (results.responseStatus === 401)
          dispatch(signOutUserAction(location.pathname));
        else if (results.responseStatus === 200)
          dispatch(gotAppointmentsAction(results.data));
      }
    };
    doGetAppointments();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user, loginedPatient]);

  useEffect(() => {
    const doGetAmount = async () => {
      let result = await getDoctorAmount(userToken);
      setDoctorAmount(result);

      result = await getPatientAmount(userToken);
      setPatientAmount(result);

      result = await getAppointmentAmount(userToken);
      setAppointmentAmount(result);

      result = await getCompletedAppointmentAmount(userToken);
      setCompletedAppointmentAmount(result);
    };
    doGetAmount();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div
      css={homePageContainer}
      className="col-12 row d-flex justify-content-around"
    >
      <div css={homePageTitle}>Добро пожаловать!</div>
      <div
        css={homePageGeneralInformation}
        className="col-bg-5 col-md-12 col-sm-12 row"
      >
        <div className="col-6">
          <div>У нас зарегистрированы:</div>
          <div>Докторов: {doctorAmount}</div>
          <div>Пациентов: {patientAmount}</div>
        </div>
        <div className="col-6">
          <div>Посещения:</div>
          <div>Всего зарегистрировано: {appointmentAmount}</div>
          <div>Завершено: {completedAppointmentAmount}</div>
        </div>
      </div>
      <div css={homePageTodayInf} className="col-bg-6 col-md-12 col-sm-12">
        Сегодня: {new Date().toISOString().substring(0, 10)}
        {appointmentsLoading ? (
          <div>Загрузка...</div>
        ) : (
          <div>
            {todayAppointments.length > 0 ? (
              <AppointmentList data={todayAppointments} />
            ) : (
              <div>На сегодня пусто</div>
            )}
          </div>
        )}
      </div>
    </div>
  );
};
