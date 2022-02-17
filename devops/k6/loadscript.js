import http from "k6/http";
import { check, group, sleep } from "k6";

const url = "http://localhost/";

export const options = {
  stages: [
    { duration: "3m", target: 100 }, // ramp-up to 50 reporters over 3 minutes
    { duration: "2m", target: 100 }, // stay at 50 users for short amount of time (peak hour)
    { duration: "3m", target: 0 }, // ramp-down to 0 users
  ],
  thresholds: {
    http_req_duration: ["p(99)<10000"], // 99% of requests must complete below 10s
  },
};

export default function () {
  http.get(url);
  sleep(1);
}
