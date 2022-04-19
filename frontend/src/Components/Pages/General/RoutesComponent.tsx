import { Fragment } from "react";
import { Route, Routes, useLocation } from "react-router-dom";
import { AppointmentPage } from "../Appointments/AppointmentPage";
import { AppointmentsListPage } from "../Appointments/AppointmentsListPage";
import { CreateIllnessPage } from "../Illneses/CreateIllnessPage";
import { DoctorListPage } from "../Doctors/DoctorListPage";
import { EditAppointmentPage } from "../Appointments/EditAppointmentPage";
import { EditDoctorPage } from "../Doctors/EditDoctorPage";
import { EditPatientPage } from "../Patients/EditPatientPage";
import { NavigationPanel } from "../../General/NavigationPanel";
import { HomePage } from "./HomePage";
import { NotFoundPage } from "./NotFoundPage";
import { PatientCardPage } from "../PatientCards/PatientCardPage";
import { ProfilePage } from "./ProfilePage";
import { RegistrationPage } from "./RegistrationPage";
import { SearchPage } from "./SearchPage";
import { SignInPage } from "./SignInPage";
import { CreateAppointmentPage } from "../Appointments/CreateAppointmentPage";
import { NotificationsPage } from "./NotificationsPage";
import { NotificationPage } from "./NotificationPage";

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
          <Route
            path="newAppointment/:doctorId"
            element={<CreateAppointmentPage />}
          />
          <Route path="addIllness" element={<CreateIllnessPage />} />
          <Route
            path="patientCard/:patientCardId"
            element={<PatientCardPage />}
          />
          <Route path="notifications" element={<NotificationsPage />} />
          <Route
            path="notifications/:notificationId"
            element={<NotificationPage />}
          />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </div>
    </Fragment>
  );
};
