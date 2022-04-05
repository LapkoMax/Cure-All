/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { getDoctor } from "../Api/DoctorsData";
import {
  gettingDoctorAction,
  gotDoctorAction,
} from "../Store/ActionCreators/DoctorActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  doctorContainer,
  doctorTitle,
} from "../Styles/Doctors/DoctorPageStyles";
import { Page } from "./General/Page";

export const DoctorPage = () => {
  const { doctorId } = useParams();
  const dispatch = useDispatch();
  const doctor = useSelector((state: AppState) => state.doctors.doctor);
  const doctorLoading = useSelector((state: AppState) => state.doctors.loading);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetDoctor = async (doctorId?: string) => {
      dispatch(gettingDoctorAction());
      let doctor = await getDoctor(userToken, doctorId);
      dispatch(gotDoctorAction(doctor));
    };
    doGetDoctor(doctorId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div>
      {doctorLoading ? (
        <div>Loading...</div>
      ) : (
        <Page>
          <div css={doctorContainer}>
            <div css={doctorTitle}>
              {doctor === null
                ? ""
                : `${doctor?.firstName} ${doctor?.lastName}`}
            </div>
          </div>
        </Page>
      )}
    </div>
  );
};
