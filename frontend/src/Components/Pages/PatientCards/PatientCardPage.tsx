/** @jsxImportSource @emotion/react */
import { Fragment, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useLocation, useParams } from "react-router-dom";
import {
  getAllAppointmentsForPatientCard,
  getAppointmentDatesForPatientCard,
  getAppointmentsForPatientCard,
} from "../../../Api/AppointmentsData";
import { getPatientCard } from "../../../Api/PatientCardsData";
import {
  gettingAppointmentsAction,
  gotAppointmentsAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientCardAction,
  gotPatientCardAction,
} from "../../../Store/ActionCreators/PatientCardActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import { PrimaryButton } from "../../../Styles/Common/Buttons";
import { FieldOption, FieldSelect } from "../../../Styles/Common/FieldStyles";
import {
  patientCardAdditionalInf,
  patientCardContainer,
  patientCardTitle,
} from "../../../Styles/PatientCards/PatientCardPageStyles";
import { AppointmentList } from "../../Appointments/AppointmentList";
import { Page } from "../../General/Page";
import { PageTitle } from "../../General/PageTitle";

export const PatientCardPage = () => {
  const { patientCardId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const patientCard = useSelector(
    (state: AppState) => state.patientCards.patientCard,
  );
  const patientCardLoading = useSelector(
    (state: AppState) => state.patientCards.loading,
  );
  const appointments = useSelector(
    (state: AppState) => state.appointments.appointments,
  );
  const appointmentsLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [dates, setDates] = useState<string[]>([]);
  const [selectedDate, setSelectedDate] = useState<string>(
    new Date().toISOString().substring(0, 10),
  );
  const [isAllAppointments, setIsAllAppointments] = useState(false);

  useEffect(() => {
    const doGetPatientCard = async (patientCardId?: string) => {
      dispatch(gettingPatientCardAction());
      let result = await getPatientCard(patientCardId, userToken);
      if (result.status === 401) dispatch(signOutUserAction(location.pathname));
      dispatch(gotPatientCardAction(result.data));
    };

    const doGetAppointments = async (patientCardId?: string) => {
      dispatch(gettingAppointmentsAction());
      let dateResults = await getAppointmentDatesForPatientCard(
        patientCardId,
        userToken,
      );

      setDates(dateResults.map((date) => date.toString().substring(0, 10)));
      let result = await getAppointmentsForPatientCard(
        patientCardId,
        userToken,
        { date: selectedDate },
      );
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotAppointmentsAction(result.data));
    };

    doGetPatientCard(patientCardId);
    doGetAppointments(patientCardId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    dates.includes(new Date().toISOString().substring(0, 10))
      ? setSelectedDate(new Date().toISOString().substring(0, 10))
      : setSelectedDate(dates[0]);
  }, [dates]);

  useEffect(() => {
    const doGetAppointments = async (patientCardId?: string) => {
      dispatch(gettingAppointmentsAction());
      let result = isAllAppointments
        ? await getAllAppointmentsForPatientCard(patientCardId, userToken)
        : await getAppointmentsForPatientCard(
            patientCardId,
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
    };
    doGetAppointments(patientCardId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedDate, isAllAppointments]);

  const onDateSelect = (e: any) => {
    setSelectedDate(e.target.value);
  };

  return (
    <Page title="Карта пациента">
      {patientCardLoading ? (
        <div>Загрузка...</div>
      ) : (
        <div css={patientCardContainer} className="row">
          <Link
            to={`/profile/${patientCard?.patientUserId}`}
            css={patientCardTitle}
            className="col-12 row"
          >
            Пациент: {patientCard?.patientFirstName}{" "}
            {patientCard?.patientLastName}
          </Link>
          <div css={patientCardAdditionalInf} className="row">
            <div>Посещения пациента:</div>
            {appointmentsLoading ? (
              <div>Загрузка...</div>
            ) : (
              <Fragment>
                <PageTitle>Посещения:</PageTitle>
                {appointments === [] ? (
                  <PageTitle>Здесь ничего нет(</PageTitle>
                ) : (
                  <Fragment>
                    <PageTitle>
                      <div className="col-12 row">
                        <div className="col-4 row d-flex justify-content-center pt-2">
                          {isAllAppointments ? "Все посещения:" : "Для даты:"}
                        </div>
                        {!isAllAppointments && (
                          <div className="col-4 row d-flex justify-content-center">
                            <FieldSelect
                              value={selectedDate}
                              onChange={onDateSelect}
                            >
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
                            isAllAppointments ? "col-5" : "col-4"
                          } row mx-2`}
                          onClick={() => {
                            setIsAllAppointments(!isAllAppointments);
                          }}
                        >
                          {isAllAppointments
                            ? "Посещения для даты"
                            : "Все посещения"}
                        </PrimaryButton>
                      </div>
                    </PageTitle>
                    <AppointmentList data={appointments} />
                  </Fragment>
                )}
              </Fragment>
            )}
          </div>
        </div>
      )}
    </Page>
  );
};
