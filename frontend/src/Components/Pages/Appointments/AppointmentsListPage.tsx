import { Fragment, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useParams } from "react-router-dom";
import {
  getAllAppointmentsForDoctor,
  getAppointmentDatesForDoctor,
  getAppointmentsForDoctor,
} from "../../../Api/AppointmentsData";
import {
  gettingAppointmentsAction,
  gotAppointmentsAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import { PrimaryButton } from "../../../Styles/Common/Buttons";
import { FieldOption, FieldSelect } from "../../../Styles/Common/FieldStyles";
import { AppointmentList } from "../../Appointments/AppointmentList";
import { Page } from "../../General/Page";
import { PageTitle } from "../../General/PageTitle";

export const AppointmentsListPage = () => {
  const { userId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const appointments = useSelector(
    (state: AppState) => state.appointments.appointments,
  );
  const appointmentsLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [dates, setDates] = useState<string[]>([]);
  const [selectedDate, setSelectedDate] = useState<string>(
    new Date().toISOString().substring(0, 10),
  );
  const [isAllAppointments, setIsAllAppointments] = useState(false);

  useEffect(() => {
    const doGetAppointments = async (userId?: string) => {
      dispatch(gettingAppointmentsAction());
      if (user?.type === "Doctor") {
        let dateResults = await getAppointmentDatesForDoctor(userId, userToken);

        setDates(dateResults.map((date) => date.toString().substring(0, 10)));
        let result = await getAppointmentsForDoctor(
          userId,
          userToken,
          selectedDate === ""
            ? undefined
            : {
                date: selectedDate,
              },
        );
        if (result.responseStatus === 401)
          dispatch(signOutUserAction(location.pathname));
        dispatch(gotAppointmentsAction(result.data));
      }
    };
    doGetAppointments(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setSelectedDate(dates[0]);
  }, [dates]);

  useEffect(() => {
    const doGetAppointments = async (userId?: string) => {
      dispatch(gettingAppointmentsAction());
      if (user?.type === "Doctor") {
        let result = isAllAppointments
          ? await getAllAppointmentsForDoctor(userId, userToken)
          : await getAppointmentsForDoctor(
              userId,
              userToken,
              selectedDate === ""
                ? undefined
                : {
                    date: selectedDate,
                  },
            );
        if (result.responseStatus === 401)
          dispatch(signOutUserAction(location.pathname));
        dispatch(gotAppointmentsAction(result.data));
      }
    };
    doGetAppointments(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedDate, isAllAppointments]);

  const onDateSelect = (e: any) => {
    setSelectedDate(e.target.value);
  };

  return (
    <div>
      {appointmentsLoading ? (
        <div>Загрузка...</div>
      ) : (
        <Page>
          {appointments === [] ? (
            <PageTitle>Здесь ничего нет(</PageTitle>
          ) : (
            <Fragment>
              <PageTitle>
                <div className="row">
                  <div className="col-5 row d-flex justify-content-center pt-2">
                    {isAllAppointments
                      ? "Все посещения:"
                      : "Посещения для даты:"}
                  </div>
                  {!isAllAppointments && (
                    <div className="col-4 row d-flex justify-content-center">
                      <FieldSelect value={selectedDate} onChange={onDateSelect}>
                        {dates.map((date) => (
                          <FieldOption key={date} value={date}>
                            {date}
                          </FieldOption>
                        ))}
                      </FieldSelect>
                    </div>
                  )}
                  <PrimaryButton
                    className={`btn btn-primary ${
                      isAllAppointments ? "col-4" : "col-3"
                    } row mx-2`}
                    onClick={() => {
                      setIsAllAppointments(!isAllAppointments);
                    }}
                  >
                    {isAllAppointments ? "Посещения для даты" : "Все посещения"}
                  </PrimaryButton>
                </div>
              </PageTitle>
              <AppointmentList data={appointments} />
            </Fragment>
          )}
        </Page>
      )}
    </div>
  );
};
