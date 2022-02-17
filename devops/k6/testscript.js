import http from "k6/http";
import { group, sleep } from "k6";

export const options = {
  vus: 100,
  duration: "600s",
};

export default function () {
  group("test k6 endpoint", function () {
    http.get("https://test.k6.io");
    sleep(1);
  });
}
