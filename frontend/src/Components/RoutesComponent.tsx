import { Fragment } from "react";
import { Route, Routes, useLocation } from "react-router-dom";
import { AppointmentPage } from "./AppointmentPage";
import { AppointmentsListPage } from "./AppointmentsListPage";
import { CreateIllnessPage } from "./CreateIllnessPage";
import { DoctorListPage } from "./DoctorListPage";
import { EditAppointmentPage } from "./EditAppointmentPage";
import { EditDoctorPage } from "./EditDoctorPage";
import { EditPatientPage } from "./EditPatientPage";
import { NavigationPanel } from "./General/NavigationPanel";
import { HomePage } from "./HomePage";
import { NotFoundPage } from "./NotFoundPage";
import { PatientCardPage } from "./PatientCardPage";
import { ProfilePage } from "./ProfilePage";
import { RegistrationPage } from "./RegistrationPage";
import { SearchPage } from "./SearchPage";
import { SignInPage } from "./SignInPage";

export const RoutesComponent = () => {
  const location = useLocation();
  return (
    <Fragment>
      {!location.pathname.includes("signin") &&
        !location.pathname.includes("register") && (
          <div className="col-2 row">
            <NavigationPanel />
          </div>
        )}
      <div
        className={
          (location.pathname.includes("signin") ||
          location.pathname.includes("register")
            ? "col-12"
            : "col-10") + " row"
        }
      >
        <Routes>
          <Route path="" element={<HomePage />} />
          <Route path="signin" element={<SignInPage />} />
          <Route path="register" element={<RegistrationPage />} />
          <Route path="search" element={<SearchPage />} />
          <Route path="profile/:userId" element={<ProfilePage />} />
          <Route path="doctors" element={<DoctorListPage />} />
          <Route path="doctors/:doctorId/edit" element={<EditDoctorPage />} />
          <Route
            path="patients/:patientId/edit"
            element={<EditPatientPage />}
          />
          <Route
            path="appointments/:userId"
            element={<AppointmentsListPage />}
          />
          <Route
            path="appointment/:appointmentId"
            element={<AppointmentPage />}
          />
          <Route
            path="appointment/:appointmentId/edit"
            element={<EditAppointmentPage />}
          />
          <Route path="addIllness" element={<CreateIllnessPage />} />
          <Route
            path="patientCard/:patientCardId"
            element={<PatientCardPage />}
          />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </div>
    </Fragment>
  );
};
